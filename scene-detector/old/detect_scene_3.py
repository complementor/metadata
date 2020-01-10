from __future__ import print_function
import os
import cv2
import numpy as np

from PIL import Image
import imagehash

STATS_FILE_PATH = 'output/testvideo.stats.csv'
# https://github.com/atduskgreg/opencv-processing


def CompareImages(img1, img2):
    # yo = cv2.diff(img1)
    # print(yo)
    # hash0 = imagehash.average_hash(Image.fromarray(imageA))
    # hash1 = imagehash.average_hash(Image.fromarray(imageB))
    # cutoff = 5
    # if hash0 - hash1 < cutoff:
    #     #print('images are similar')
    #     return True
    # else:
    #     #print('images are not similar')
    #     return False

    # take the absolute difference of the images
    res = cv2.absdiff(img1, img2)

    # convert the result to integer type
    res = res.astype(np.uint8)

    # find percentage difference based on number of pixels that are not zero
    percentage = (np.count_nonzero(res) * 100) / res.size
    print(percentage)
    if percentage > 99:
        return False
    else:
        return True

    # i1 = Image.fromarray(img1)
    # i2 = Image.fromarray(img2)
    # assert i1.mode == i2.mode, "Different kinds of images."
    # assert i1.size == i2.size, "Different sizes."

    # pairs = zip(i1.getdata(), i2.getdata())
    # if len(i1.getbands()) == 1:
    #     # for gray-scale jpegs
    #     dif = sum(abs(p1-p2) for p1, p2 in pairs)
    # else:
    #     dif = sum(abs(c1-c2) for p1, p2 in pairs for c1, c2 in zip(p1, p2))

    # ncomponents = i1.size[0] * i1.size[1] * 3
    # print("Difference (percentage):", (dif / 255.0 * 100) / ncomponents)
    # difference = (dif / 255.0 * 100) / ncomponents
    # if difference > 10:
    #     return False
    # else:
    #     return True


def main():
    vs = cv2.VideoCapture('test_sample.mp4')

    try:

        i = 0
        while True:

            # grab a frame from the video
            (grabbed, frame) = vs.read()
            #i += 1

            orig = frame.copy()
            # filename = "{}.png".format(i)

            if not os.listdir("output/"):
                i += 1
                print("Directory is empty")
                cv2.imwrite("output/{}.png".format(i), orig)
                print("[INFO] saving {}".format(i))
            else:
                # print("Directory is not empty")

                for file in os.listdir("output/"):
                    fileNameNumber = os.path.splitext(file)[0]
                    if int(fileNameNumber) < i:
                        # if int(fileNameNumber) == i:
                        continue
                    else:
                        print("file: ", fileNameNumber)
                        # if file is not None:
                        # print("file: {}".format(file))
                        img = cv2.imread("output/" + file)
                        # cv2.imshow("disk", img)
                        # print("orig: ", orig)
                        # print("img: ", img)
                        # print("Comparing: ", i)
                        exists = CompareImages(img, orig)
                        if exists is not True:
                            i += 1
                            print("Result: ", exists)
                            cv2.imwrite("output/{}.png".format(i), orig)
                            print("[INFO] saving {}".format(i))
                    # else:
                    #     # cv2.imwrite("output/{}.png".format(i), orig)
                    #     print("[INFO] saving {}".format(i))

            # if the frame is None, then we have reached the end of the
            if frame is None:
                break

        # print('List of scenes obtained:')
        # for i, scene in enumerate(scene_list):
        #     print('    Scene %2d: Start %s / Frame %d, End %s / Frame %d' % (
        #         i+1,
        #         scene[0].get_timecode(), scene[0].get_frames(),
        #         scene[1].get_timecode(), scene[1].get_frames(),))
        #     #
        #     vs.set(1, scene[0].get_frames())
        #     (grabbed, frame) = vs.read()
        #     orig = frame.copy()
        #     cv2.imwrite("output/test{}.png".format(i), orig)

        # # We only write to the stats file if a save is required:
        # if stats_manager.is_save_required():
        #     with open(STATS_FILE_PATH, 'w') as stats_file:
        #         stats_manager.save_to_csv(stats_file, base_timecode)

    finally:
        vs.release()


if __name__ == "__main__":
    main()
