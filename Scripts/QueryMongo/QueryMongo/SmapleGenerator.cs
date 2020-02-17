using MongoDB.Bson;

namespace QueryMongo
{
    public static class Samples
    {
        public static BsonDocument HubWithScenes
        {
            get => new BsonDocument
                {
                    { "hub", new BsonDocument
                    {
                        { "date", "14-02-2020 13:50:23" } ,
                        { "satellite", new BsonDocument
                        {
                            { "video_information", new BsonDocument
                            {
                                { "scenes", 50 },
                                { "frames", 5059},
                                { "duration", "00:03:31.002"},
                                { "subtitles", new BsonArray {}
                }
            }}}}}}};
        }
        public static BsonDocument With1AlgmDoc
        {
            get => new BsonDocument
                {
                    { "hub", new BsonDocument
                    {
                        { "date", "14-02-2020 13:50:23" } ,
                        { "satellite", new BsonDocument
                        {
                            { "video_information", new BsonDocument
                            {
                                { "scenes", 50 },
                                { "frames", 5059},
                                { "duration", "00:03:31.002"},
                                { "subtitles", new BsonArray {}}
                            }
                            },
                            { "optical_character_recognition", new BsonArray
                            {
                                new BsonDocument {
                                        { "scene", 1},
                                        { "start", "00:00:00.834"},
                                        { "end", "00:00:14.348"},
                                        { "frameStart", 20},
                                        { "frameEnd", 344},
                                        { "value", "Some OCR generated text"},
                                    }
                            }
                            }
            }}}}};
        }
        public static BsonDocument With2AlgmDoc
        {
            get => new BsonDocument
                {
                    { "hub", new BsonDocument
                    {
                        { "date", "14-02-2020 13:50:23" } ,
                        { "satellite", new BsonDocument
                        {
                            { "video_information", new BsonDocument
                            {
                                { "scenes", 50 },
                                { "frames", 5059},
                                { "duration", "00:03:31.002"},
                                { "subtitles", new BsonArray {}
                            }}},
                            { "optical_character_recognition", new BsonArray
                            {
                                new BsonDocument {
                                        { "scene", 1},
                                        { "start", "00:00:00.834"},
                                        { "end", "00:00:14.348"},
                                        { "frameStart", 20},
                                        { "frameEnd", 344},
                                        { "value", ""},
                                    },
                                }
                            },
                            { "speech_recognition", new BsonArray
                            {
                                new BsonDocument {
                                    { "scene", 1},
                                    { "start", "00:00:00.834"},
                                    { "end", "00:00:14.348"},
                                    { "frameStart", 20},
                                    { "frameEnd", 344},
                                    { "value", "you better than other video with the girls get something on YouTube but I think I'm taking it might be incredibly useful I still don't really believe it when I saw this online and I said a demonstration as a couple"},
                                },
                            }
                            }}}
            }}};

        }
        public static BsonDocument With3AlgmDoc
        {
            get => new BsonDocument
                {
                    { "hub", new BsonDocument
                    {
                        { "date", "14-02-2020 13:50:23" } ,
                        { "satellite", new BsonDocument
                        {
                            { "video_information", new BsonDocument
                            {
                                { "scenes", 50 },
                                { "frames", 5059},
                                { "duration", "00:03:31.002"},
                                { "subtitles", new BsonArray {}
},
                            { "collaborative_metadata", new BsonArray {} },
                            { "optical_character_recognition", new BsonArray
                            {
                                new BsonDocument {
                                        { "scene", 1},
                                        { "start", "00:00:00.834"},
                                        { "end", "00:00:14.348"},
                                        { "frameStart", 20},
                                        { "frameEnd", 344},
                                        { "value", ""},
                                    },
                                }
                            },
                            { "speech_recognition", new BsonArray
                            {
                                new BsonDocument {
                                    { "scene", 1},
                                    { "start", "00:00:00.834"},
                                    { "end", "00:00:14.348"},
                                    { "frameStart", 20},
                                    { "frameEnd", 344},
                                    { "value", "you better than other video with the girls get something on YouTube but I think I'm taking it might be incredibly useful I still don't really believe it when I saw this online and I said a demonstration as a couple"},
                                },
                            }
                            },
                            { "sentiment_analysis",  new BsonArray
                            {
                                new BsonDocument
                                {
                                    { "scene", 1},
                                    { "start", "00:00:00.834"},
                                    { "end", "00:00:14.348"},
                                    { "frameStart", 20},
                                    { "frameEnd", 344},
                                    { "value", new BsonDocument
                                        {
                                            { "neg", 0.0 },
                                            { "neu", 0.803 },
                                            { "pos", 0.197 },
                                            { "compound", 0.7808 },
                                        }
                                    }
                                }
                            }
                            }}}}}}}};
        }
        public static BsonDocument With4AlgmDoc
        {
            get => new BsonDocument
                {
                    { "hub", new BsonDocument
                    {
                        { "date", "14-02-2020 13:50:23" } ,
                        { "satellite", new BsonDocument
                        {
                            { "video_information", new BsonDocument
                            {
                                { "scenes", 50 },
                                { "frames", 5059},
                                { "duration", "00:03:31.002"},
                                { "subtitles", new BsonArray {}
},
                            { "collaborative_metadata", new BsonArray {} },
                            { "optical_character_recognition", new BsonArray
                            {
                                new BsonDocument {
                                        { "scene", 1},
                                        { "start", "00:00:00.834"},
                                        { "end", "00:00:14.348"},
                                        { "frameStart", 20},
                                        { "frameEnd", 344},
                                        { "value", ""},
                                    },
                                }
                            },
                            { "speech_recognition", new BsonArray
                            {
                                new BsonDocument {
                                    { "scene", 1},
                                    { "start", "00:00:00.834"},
                                    { "end", "00:00:14.348"},
                                    { "frameStart", 20},
                                    { "frameEnd", 344},
                                    { "value", "you better than other video with the girls get something on YouTube but I think I'm taking it might be incredibly useful I still don't really believe it when I saw this online and I said a demonstration as a couple"},
                                },
                            }
                            },
                            { "sentiment_analysis",  new BsonArray
                            {
                                new BsonDocument
                                {
                                    { "scene", 1},
                                    { "start", "00:00:00.834"},
                                    { "end", "00:00:14.348"},
                                    { "frameStart", 20},
                                    { "frameEnd", 344},
                                    { "value", new BsonDocument
                                        {
                                            { "neg", 0.0 },
                                            { "neu", 0.803 },
                                            { "pos", 0.197 },
                                            { "compound", 0.7808 },
                                        }
                                    }
                                }
                            }
                            },
                            { "objects", new BsonArray
                            {
                                new BsonDocument
                                {
                                    { "scene", 1},
                                    { "start", "00:00:00.834"},
                                    { "end", "00:00:14.348"},
                                    { "frameStart", 20},
                                    { "frameEnd", 344},
                                    { "value", new BsonArray
                                    {
                                        new BsonDocument
                                        {
                                            { "label", "person"},
                                            { "confidence", 0.9997119307518005},
                                        },
                                        new BsonDocument
                                        {
                                            { "label", "dining table"},
                                            { "confidence", 0.5953575372695923 },
            }}}}}}}}}}}}};
        }
    }
}
