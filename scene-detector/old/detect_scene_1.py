# USAGE
# python detect_scene.py --video traffic-mini.mp4 --output output      

# import the necessary packages
import argparse
import imutils
import cv2
import os
import matplotlib.pyplot as plt
import cvlib as cv
from cvlib.object_detection import draw_bbox
import json

# construct the argument parser and parse the arguments
ap = argparse.ArgumentParser()
ap.add_argument("-v", "--video", required=True, type=str,
                help="path to input video file")
ap.add_argument("-o", "--output", required=True, type=str,
                help="path to output directory to store frames")
ap.add_argument("-p", "--min-percent", type=float, default=1.0,
                help="lower boundary of percentage of motion")
ap.add_argument("-m", "--max-percent", type=float, default=10.0,
                help="upper boundary of percentage of motion")
ap.add_argument("-w", "--warmup", type=int, default=0,
                help="# of frames to use to build a reasonable background model")
args = vars(ap.parse_args())

# initialize the background subtractor
fgbg = cv2.bgsegm.createBackgroundSubtractorGMG()

# initialize a boolean used to represent whether or not a given frame
# has been captured along with two integer counters -- one to count
# the total number of frames that have been captured and another to
# count the total number of frames processed
captured = False
total = 0
frames = 0

# open a pointer to the video file initialize the width and height of
# the frame
vs = cv2.VideoCapture(args["video"])
(W, H) = (None, None)

data = {}

frame_count = int(vs.get(cv2.CAP_PROP_FRAME_COUNT))
print('Frame count:', frame_count)

# loop over the frames of the video
while True:
    # grab a frame from the video
    (grabbed, frame) = vs.read()

    # if the frame is None, then we have reached the end of the
    # video file
    if frame is None:
        break

    # clone the original frame (so we can save it later), resize the
    # frame, and then apply the background subtractor
    orig = frame.copy()
    frame = imutils.resize(frame, width=600)
    mask = fgbg.apply(frame)

    # apply a series of erosions and dilations to eliminate noise
    mask = cv2.erode(mask, None, iterations=2)
    mask = cv2.dilate(mask, None, iterations=2)

    # if the width and height are empty, grab the spatial dimensions
    if W is None or H is None:
        (H, W) = mask.shape[:2]

    # compute the percentage of the mask that is "foreground"
    p = (cv2.countNonZero(mask) / float(W * H)) * 100

    # if there is less than N% of the frame as "foreground" then we
    # know that the motion has stopped and thus we should grab the
    # frame
    if p < args["min_percent"] and not captured and frames > args["warmup"]:
        # show the captured frame and update the captured bookkeeping
        # variable
        cv2.imshow("Captured", frame)
        captured = True

        # construct the path to the output frame and increment the
        # total frame counter
        filename = "{}.png".format(total)
        path = os.path.sep.join([args["output"], filename])

        # save the  *original, high resolution* frame to disk
        print("[INFO] saving {}".format(path))
        cv2.imwrite(path, orig)

        # object detection in current scene
        im = cv2.imread(path)
        bbox, label, conf = cv.detect_common_objects(im)
        output_image = draw_bbox(im, bbox, label, conf)

        # save object detection results as an image
        resultPath = os.path.sep.join(
            [args["output"], "{}.jpg".format(total)])
        cv2.imwrite(resultPath, output_image)

        # write results to json file
        jsonFile = "{}.json".format(total)
        with open(os.path.sep.join([args["output"], jsonFile]), "w") as json_file:
            #print(f"{label}, {conf}", file=text_file)
            # plt.imshow(output_image)
            # plt.show()
            data['objects'] = []
            for l, c, xy in zip(label, conf, bbox):
                data['objects'].append({
                    'label': l,
                    'confidence': c,
                    'coordinates': xy
                })
            json.dump(data, json_file)

            total += 1

    # otherwise, either the scene is changing or we're still in warmup
    # mode so let's wait until the scene has settled or we're finished
    # building the background model
    elif captured and p >= args["max_percent"]:
        captured = False

    # display the frame and detect if there is a key press
    cv2.imshow("Frame", frame)
    cv2.imshow("Mask", mask)
    key = cv2.waitKey(1) & 0xFF

    # if the `q` key was pressed, break from the loop
    if key == ord("q"):
        break

    # increment the frames counter
    frames += 1

# do a bit of cleanup
vs.release()
cv2.destroyAllWindows()
