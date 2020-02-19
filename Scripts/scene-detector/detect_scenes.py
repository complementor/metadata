from __future__ import print_function
import os

import scenedetect
from scenedetect.video_manager import VideoManager
from scenedetect.scene_manager import SceneManager
from scenedetect.frame_timecode import FrameTimecode
from scenedetect.stats_manager import StatsManager
from scenedetect.detectors import ContentDetector
from scenedetect.detectors import ThresholdDetector

import json
import cv2
import cvlib as cv
import numpy as np
from datetime import datetime, timedelta

from cvlib.object_detection import draw_bbox

from imutils.object_detection import non_max_suppression
import pytesseract

import speech_recognition as spreg
import moviepy.editor
from pydub import AudioSegment
AudioSegment.ffmpeg = "./ffmpeg/bin/"

# https://medium.com/@b.terryjack/nlp-pre-trained-sentiment-analysis-1eb52a9d742c
import nltk
nltk.download('vader_lexicon')
from nltk.sentiment.vader import SentimentIntensityAnalyzer

import argparse

ap = argparse.ArgumentParser()

ap.add_argument("-v", "--video", required=False, type=str, default="P:/resources/YoutubeToBeProcessed/",
                help="path to input video file")

ap.add_argument("-o", "--output", required=False, type=str, default="P:/resources/YoutubeToBeProcessed/metadata/",
                help="path to output directory")


consoleArgs = vars(ap.parse_args())

# example: --video P:/resources/YoutubeToBeProcessed/5267249c-c0db-43cb-80d6-ddd3c2a2beeb.mp4 --output P:/resources/YoutubeToBeProcessed/metadata

fileName = consoleArgs["video"].rsplit('\\', 1)[-1]
print("show me",fileName)
fileName = fileName.split('.')[0]
print("show me",fileName)
args = {
    "targetVideo": consoleArgs["video"],
    "outputFolder": consoleArgs["output"],
    "outputJsonFile": str(fileName + ".json"),
    "mp3FileLocation": str(consoleArgs["output"] + "audio/" +  fileName + ".mp3"),
    "wavFileLocation": str(consoleArgs["output"] + "audio/" +  fileName + ".wav"),
    "startAnalysis": 20,
    "endAnalysis": 2000000000000000000.0,
    "sceneThreshold": 30,
    "minSceneLength": 1
}

print("show me folder",[args["outputJsonFile"]])
print("show me mp3 ",[args["mp3FileLocation"]])
print("show me wav",[args["wavFileLocation"]])

def main():
    # start execution timer
    startTime = datetime.now()
    print("start: ", startTime.strftime("%d-%m-%Y %H:%M:%S"))
    print("\n")
    
    # init scene detector.
    video_manager = VideoManager([args["targetVideo"]])
    stats_manager = StatsManager()
    scene_manager = SceneManager(stats_manager)
    scene_manager.add_detector(ContentDetector(threshold=args["sceneThreshold"], min_scene_len=args["minSceneLength"]))
    base_timecode = video_manager.get_base_timecode()
    
    # init regular opencv module.
    vc = cv2.VideoCapture(args["targetVideo"])

    # build json structure. 
    jsonDictionary = {
        "Id": fileName,     
        "hub": {
           "date": datetime.now().strftime("%d-%m-%Y %H:%M:%S"),
           "satellite": []
        }
    }
    
    # process dictionary and return as string
    jsonString = json.dumps(jsonDictionary)
    
    # return queryable dictionary
    jsonData = json.loads(jsonString)
    
    # json location & filename
    jsonFile = args["outputJsonFile"]

    try:
        # get audio file     
        get_audio_file(args)
        
        # detect scenes
        ds = Detect_scenes()
        ds.run(base_timecode, video_manager, scene_manager, vc, jsonData)
      
        with open(os.path.sep.join([args["outputFolder"], jsonFile]), "w") as json_file:
            json.dump(jsonData, json_file)

    finally:
        video_manager.release()
        vc.release()
        print("\n")
        print("Execution time: ", datetime.now() - startTime)

class Detect_scenes:     
    def run(self, base_timecode, video_manager, scene_manager, vc, jsonData):
        print("\n")
        print('Detecting scenes..')
        
        # specify time frame for analysis. Videos longer than end_time will not be processed fully.
        start_time = base_timecode + args["startAnalysis"]
        end_time = base_timecode + args["endAnalysis"]
        video_manager.set_duration(start_time=start_time, end_time=end_time)
    
        # set downscale factor to improve processing speed.
        video_manager.set_downscale_factor()
    
        # start video_manager.
        video_manager.start()
    
        # perform scene detection on video_manager.
        scene_manager.detect_scenes(frame_source=video_manager)
    
        # obtain list of detected scenes.
        scene_list = scene_manager.get_scene_list(base_timecode)
        
        # add chosen algorithms to the structure as satellites
        self.__add_new_algorithm_satellite(jsonData, "common_objects")
        
        self.__add_new_algorithm_satellite(jsonData, "optical_character_recognition")
        
        self.__add_new_algorithm_satellite(jsonData, "speech_recognition")
        
        self.__add_new_algorithm_satellite(jsonData, "sentiment_analysis")
        
        # iterate the list of scenes.
        print("\n")
        print('List of scenes obtained:')
        for i, scene in enumerate(scene_list):
            print("\n")
            print('    Scene %2d: Start %s / Frame %d, End %s / Frame %d' % (
                i+1,
                scene[0].get_timecode(), scene[0].get_frames(),
                scene[1].get_timecode(), scene[1].get_frames(),))
    
            # use regular opencv module to jump to specific scene and capture the first frame (beginning of scene).
            vc.set(1, scene[0].get_frames())
            (captured, frame) = vc.read()
            
            if captured:
                              
                # run algorithms on current scene              
                self.__detect_common_objects(frame, jsonData, i, scene)
                
                self.__detect_optical_character_recognition(frame, jsonData, i, scene)
                
                self.__run_speech_recognition_on_scene(args, jsonData, scene, i, fileName)
          
    def __add_new_algorithm_satellite(self, jsonData, algorithmName):
        jsonData["hub"]["satellite"].append({
            "{}".format(algorithmName): []
        })    

    def __add_object_to_algorithm_satellite(self, scene, jsonData, i, algorithmName, value, number):
        jsonData["hub"]["satellite"][number][algorithmName].append({
            "scene": i + 1,
            "start": scene[0].get_timecode(),
            "end": scene[1].get_timecode(),
            "frameStart": scene[0].get_frames(),
            "frameEnd": scene[1].get_frames(),
            "value": value
        })                
            
    def __detect_common_objects(self, frame, jsonData, i, scene):
              
        bbox, label, conf1 = cv.detect_common_objects(frame.copy())
        
        value = []
        for l, c, xy in zip(label, conf1, bbox):
            value.append({'label': l,'confidence': c })
            
        self.__add_object_to_algorithm_satellite(scene, jsonData, i, "common_objects", value, 0)
         
       
    def __detect_optical_character_recognition(self, frame, jsonData, i, scene):
        image = frame.copy()
        #image = cv2.imread("./testocr.png")
        
        pytesseract.pytesseract.tesseract_cmd = r'P:/src/Scripts/scene-detector/tesseract/tesseract.exe'
        text = pytesseract.image_to_string(image, lang='eng')
        
        self.__add_object_to_algorithm_satellite(scene, jsonData, i, "optical_character_recognition", text, 1)
        
    def __run_speech_recognition_on_scene(self, args, jsonData, scene, i, fileName):   
        scene_start = scene[0].get_timecode()
        scene_end = scene[1].get_timecode()
        start = (datetime.strptime(scene_start+'000', '%H:%M:%S.%f') - datetime.strptime('00', '%H')).total_seconds()*1000
        end = (datetime.strptime(scene_end+'000', '%H:%M:%S.%f') - datetime.strptime('00', '%H')).total_seconds()*1000
        
        print(str("Zi-mi cei aici :/" + args["wavFileLocation"]))
        audioFile = AudioSegment.from_file(str(args["wavFileLocation"]))

        print(str("Zi-mi cei aici :/" + args["wavFileLocation"]))
        audiofileSegmentName = str(fileName + "_") + str(i + 1) + ".wav"
        fileLocation = args["outputFolder"] + str("audio/" + audiofileSegmentName)
        slice = audioFile[start:end]
        slice.export(fileLocation, format="wav")
          
        print(str("Zi-mi cei aici :/" + fileLocation)) 
        recog = spreg.Recognizer()
        with spreg.AudioFile(fileLocation) as source:
           speech = recog.record(source) 
           print("\n")
           print("Running speech recognition on scene " + str(i + 1) + "..")
           try:
              text = recog.recognize_google(speech)
              print("\n")
              print(text)
              
              self.__add_object_to_algorithm_satellite(scene, jsonData, i, "speech_recognition", text, 2)
              
              self.__run_sentiment_analysis(jsonData, text, i, scene)
              
           except spreg.UnknownValueError:
              print('Unable to recognize the audio in scene ' + str(i + 1))
           except spreg.RequestError as e: 
              print("Request error from Google Speech Recognition service; {}".format(e))
              
    def __run_sentiment_analysis(self, jsonData, text, i, scene):
        sid = SentimentIntensityAnalyzer()
        sentiment_analysis = sid.polarity_scores(text)
        print("\n")
        print("sentiment_analysis: ", sentiment_analysis)
        self.__add_object_to_algorithm_satellite(scene, jsonData, i, "sentiment_analysis", sentiment_analysis, 3)
        
def get_audio_file(args):
    video = moviepy.editor.VideoFileClip(args["targetVideo"])

    audio = video.audio
    audio.write_audiofile(args["mp3FileLocation"])  
    
    mp3 = AudioSegment.from_mp3(args["mp3FileLocation"])
    mp3.export(args["wavFileLocation"], format="wav")
    
    video.reader.close()
    video.audio.reader.close_proc()

if __name__ == "__main__":
    main()
