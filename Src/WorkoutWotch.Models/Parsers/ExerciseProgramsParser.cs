﻿namespace WorkoutWotch.Models.Parsers
{
    using System.Linq;
    using Services.Contracts.Audio;
    using Services.Contracts.Delay;
    using Services.Contracts.Logger;
    using Services.Contracts.Speech;
    using Sprache;
    using Utility;

    internal static class ExerciseProgramsParser
    {
        public static Parser<ExercisePrograms> GetParser(
            IAudioService audioService,
            IDelayService delayService,
            ILoggerService loggerService,
            ISpeechService speechService)
        {
            Ensure.ArgumentNotNull(audioService, nameof(audioService));
            Ensure.ArgumentNotNull(delayService, nameof(delayService));
            Ensure.ArgumentNotNull(loggerService, nameof(loggerService));
            Ensure.ArgumentNotNull(speechService, nameof(speechService));

            return
                from _ in VerticalSeparationParser.Parser
                from exercisePrograms in ExerciseProgramParser.GetParser(audioService, delayService, loggerService, speechService).DelimitedBy(Parse.WhiteSpace.Many()).Optional()
                from __ in Parse.WhiteSpace.Many().End()
                select new ExercisePrograms(exercisePrograms.GetOrElse(Enumerable.Empty<ExerciseProgram>()));
        }
    }
}