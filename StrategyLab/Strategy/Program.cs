using CommandLine;
using Strategy.Data.Hall;
using Strategy.Data.Logging;
using Strategy.Data.Rating;
using Strategy.Domain.Hall;
using Strategy.Domain.Princess;
using Strategy.Domain.Princess.Strategy;

namespace Strategy
{
    internal class Options
    {
        [Option('f', "file", Required = true, HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }
    }

    static class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(OnParseSuccess);
        }

        private static void OnParseSuccess(Options opts)
        {
            var applicants = ApplicantsFactory.RandomApplicants(100);
            var ratingService = new RatingServiceImpl(applicants);
            var strategy = new FriendStrategyImpl(ratingService, 10);
            var hall = new QueueHallImpl(applicants);
            var logger = new FileLogger("result.txt");
            var princess = new Princess(hall, strategy, logger);

            var applicant = princess.StartDating();
            
            logger.LogRating(applicant != null ? ratingService.GetRating(applicant) : 10);
            logger.Dispose();
        }
    }
}