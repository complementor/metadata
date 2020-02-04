Param(
    $Url,
    $Destination,
    $Name
)
& "C:\Programming\FFmpeg\bin\youtube-dl.exe" --xattrs $Url -o $Destination$Name

# & "C:\Programming\FFmpeg\bin\youtube-dl.exe" --add-metadata https://www.youtube.com/watch?v=AgGsLJIIjWs -o "p:\resources\youtube\5"
# .\ExtractYouTubeMetadata.ps1 -Url https://www.youtube.com/watch?v=AgGsLJIIjWs -Destination "p:\resources\youtube\" -Name "6"

# 56.com
# .\ExtractYouTubeMetadata.ps1 -Url http://www.56.com/w36/play_album-aid-14488424_vid-MTUwMjQ0Mzcz.html -Destination "p:\resources\youtube\" -Name "7"

# 9gag.com
# & "p:\src\Scripts\youtubeExtraction\ExtractYouTubeMetadata.ps1" -Url https://9gag.com/gag/aBgomXZ -Destination "p:\resources\youtube\" -Name "7"


# Download entire series season keeping each series and each season in separate directory under C:/MyVideos
# $ youtube-dl -o "C:/MyVideos/%(series)s/%(season_number)s - %(season)s/%(episode_number)s - %(episode)s.%(ext)s" https://videomore.ru/kino_v_detalayah/5_sezon/367617


#from udemy
# $ youtube-dl -u user -p password -o '~/MyVideos/%(playlist)s/%(chapter_number)s - %(chapter)s/%(title)s.%(ext)s' https://www.udemy.com/java-tutorial/

