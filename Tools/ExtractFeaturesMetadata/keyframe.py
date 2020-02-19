# -*- coding: utf-8 -*-
"""
Created on Thu Feb 13 13:06:37 2020

@author: guru
"""

from Katna.video import Video
import os

# For windows, the below if condition is must.
if __name__ == "__main__":

  #instantiate the video class
  vd = Video()

  #number of key-frame images to be extracted
  no_of_frames_to_returned = 100

  #Input Video file path
  video_file_path = "P:/src/Scripts/scene-detector/videos/football.mp4"

  #Call the public key-frame extraction method
  imgs = vd.extract_frames_as_images(no_of_frames = no_of_frames_to_returned, \
       file_path= video_file_path)

  # Make folder for saving frames
  output_folder_video_image = 'P:/src/Scripts/scene-detector/output'
  

  # Save all frames to disk
  for counter,img in enumerate(imgs):
       vd.save_frame_to_disk(img, file_path=output_folder_video_image, \
            file_name="test_"+str(counter), file_ext=".jpeg")