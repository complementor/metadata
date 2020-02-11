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
import datetime

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

#conda install -c conda-forge ffmpeg

#from multiprocessing import Process
#import multiprocessing as mp
#print("Number of processors: ", mp.cpu_count())

args = {
    "targetVideo": "videos/football.mp4",
    "outputFolder": "output/",
    "outputJsonFile": "document_id.json",
    "mp3FileLocation": "audio.mp3",
    "wavFileLocation": "audioConverted.wav",
    "startAnalysis": 20,
    "endAnalysis": 2000000000000000000.0,
    "sceneThreshold": 30,
    "minSceneLength": 1
}

def main():
    # start execution timer
    startTime = datetime.datetime.now()
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
        "document_id": 'guid',
        "hub": {
           "date": datetime.datetime.now().strftime("%d-%m-%Y %H:%M:%S"),
           "satellite": {
               "video_information": {
                   "scenes": 0,
                   "frames": 0,
                   "duration": "",
                   "subtitles": []
                },
                "collaborative_metadata": [],
                "low_level_features": [],
                "speech_recognition": "",
                "sentiment_analysis": "",
                "scenes_detected": []
            }
        }
    }
    
    # process dictionary and return as string
    jsonString = json.dumps(jsonDictionary)
    
    # return queryable dictionary
    jsonData = json.loads(jsonString)
    
    # json location & filename
    jsonFile = args["outputJsonFile"]

    try:
        # speech recognition     
        run_speech_recognition(args, jsonData)
        
        # detect scenes
        ds = Detect_scenes()
        ds.run(base_timecode, video_manager, scene_manager, vc, jsonData)
      
        with open(os.path.sep.join([args["outputFolder"], jsonFile]), "w") as json_file:
            json.dump(jsonData, json_file)

    finally:
        video_manager.release()
        vc.release()
        print("\n")
        print("Execution time: ", datetime.datetime.now() - startTime)

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
        
        # iterate the list of scenes.
        print("\n")
        print('List of scenes obtained:')
        for i, scene in enumerate(scene_list):
            print('    Scene %2d: Start %s / Frame %d, End %s / Frame %d' % (
                i+1,
                scene[0].get_timecode(), scene[0].get_frames(),
                scene[1].get_timecode(), scene[1].get_frames(),))
    
            # use regular opencv module to jump to specific scene and capture the first frame (beginning of scene).
            vc.set(1, scene[0].get_frames())
            (captured, frame) = vc.read()
            
            if captured:
                
                # video information
                jsonData["hub"]["satellite"]["video_information"]["scenes"] = i + 1
                jsonData["hub"]["satellite"]["video_information"]["frames"] = scene[1].get_frames()
                jsonData["hub"]["satellite"]["video_information"]["duration"] = scene[1].get_timecode()
                    
                self.__create_scene_json_object(scene, jsonData, i)
                
                self.__detect_common_objects(frame, jsonData, i)
                
                self.__detect_optical_character_recognition(frame, jsonData, i)
                
                #extract_features(frame, outputFolder, i)
          
    def __create_scene_json_object(self, scene, jsonData, i):
        jsonData["hub"]["satellite"]["scenes_detected"].append({
            'scene': i + 1,
            'start': scene[0].get_timecode(),
            'end': scene[1].get_timecode(),
            'frameStart': scene[0].get_frames(),
            'frameEnd': scene[1].get_frames(),
            "optical_character_recognition": "",
            'objects': []
        })                   
            
    def __detect_common_objects(self, frame, jsonData, i):
        bbox, label, conf1 = cv.detect_common_objects(frame.copy())
    
        for l, c, xy in zip(label, conf1, bbox):
            jsonData["hub"]["satellite"]["scenes_detected"][i]['objects'].append({
                'label': l,
                'confidence': c,
                #'bbox': xy
            })
         
       
    def __detect_optical_character_recognition(self, frame, jsonData, i):
        image = frame.copy()
        #image = cv2.imread("./testocr.png")
        
        pytesseract.pytesseract.tesseract_cmd = r'C:\Program Files\Tesseract-OCR\tesseract.exe'
        text = pytesseract.image_to_string(image, lang='eng')
        
        jsonData["hub"]["satellite"]["scenes_detected"][i]['optical_character_recognition'] = text
    
def run_speech_recognition(args, jsonData):
    video = moviepy.editor.VideoFileClip(args["targetVideo"])

    audio = video.audio
    audio.write_audiofile(args["mp3FileLocation"])  
    
    mp3 = AudioSegment.from_mp3(args["mp3FileLocation"])
    mp3.export(args["wavFileLocation"], format="wav")
    
    video.reader.close()
    video.audio.reader.close_proc()
    
    sound_file = args["wavFileLocation"]
    recog = spreg.Recognizer()
    with spreg.AudioFile(sound_file) as source:
       speech = recog.record(source) 
       print("\n")
       print("Running speech recognition..")
       try:
          text = recog.recognize_google(speech)
          print("\n")
          print(text)
          jsonData["hub"]["satellite"]["speech_recognition"] = text
          
          sid = SentimentIntensityAnalyzer()
          sentiment_analysis = sid.polarity_scores(text)
          print("\n")
          print("sentiment_analysis: ", sentiment_analysis)
          jsonData["hub"]["satellite"]["sentiment_analysis"] = sentiment_analysis
          
       except spreg.UnknownValueError:
          print('Unable to recognize the audio')
       except spreg.RequestError as e: 
          print("Request error from Google Speech Recognition service; {}".format(e))
      
def extract_features(frame, outputFolder, i):
    ##img = cv2.imread(frame)
    gray= cv2.cvtColor(frame.copy(),cv2.COLOR_BGR2GRAY)
    
    sift = cv.xfeatures2d.SIFT_create()
    kp, des = sift.detectAndCompute(gray,None)
    
    img=cv2.drawKeypoints(gray,kp)
    
    cv2.imwrite(outputFolder + "{}.jpg".format(i), img)


if __name__ == "__main__":
    main()
