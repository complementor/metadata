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
from cvlib.object_detection import draw_bbox


def main():

    # paths
    targetVideo = "videos/goldeneye.mp4"
    outputFolder = "output/"

    # init scene detector.
    video_manager = VideoManager([targetVideo])
    stats_manager = StatsManager()
    scene_manager = SceneManager(stats_manager)
    scene_manager.add_detector(ContentDetector(threshold=30, min_scene_len=1))
    base_timecode = video_manager.get_base_timecode()

    # json data.
    data = {}

    # init regular opencv module.
    vc = cv2.VideoCapture(targetVideo)

    try:
        # set
        start_time = base_timecode + 20
        end_time = base_timecode + 2000000000000000000.0
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
        print('List of scenes obtained:')
        for i, scene in enumerate(scene_list):
            print('    Scene %2d: Start %s / Frame %d, End %s / Frame %d' % (
                i+1,
                scene[0].get_timecode(), scene[0].get_frames(),
                scene[1].get_timecode(), scene[1].get_frames(),))

            # use regular opencv module to jump to specific scene and capture the first frame (beginning of scene) as png.
            vc.set(1, scene[0].get_frames())
            (captured, frame) = vc.read()
            if captured:
                # create png image.
                cv2.imwrite(outputFolder + "{}.png".format(i), frame.copy())

                # object detection in captured frame.
                im = cv2.imread(outputFolder + "{}.png".format(i))
                bbox, label, conf = cv.detect_common_objects(im)
                output_image = draw_bbox(im, bbox, label, conf)

                # save object detection results as an image.
                cv2.imwrite(outputFolder + "{}.jpg".format(i), output_image)

                # write results to json file
                jsonFile = "{}.json".format(i)
                with open(os.path.sep.join([outputFolder, jsonFile]), "w") as json_file:

                    # write scene information
                    data['scene'] = []
                    data['scene'].append({
                        'number': i,
                        'start': scene[0].get_timecode(),
                        'end': scene[1].get_timecode()
                    })

                    # write scene objects
                    data['objects'] = []
                    for l, c, xy in zip(label, conf, bbox):
                        data['objects'].append({
                            'label': l,
                            'confidence': c,
                            # 'coordinates': xy
                        })

                    json.dump(data, json_file)

    finally:
        video_manager.release()
        vc.release()


if __name__ == "__main__":
    main()
