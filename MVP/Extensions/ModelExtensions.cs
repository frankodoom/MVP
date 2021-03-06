﻿using System;
using System.Collections.Generic;
using System.Linq;
using MVP.Models;
using MvvmHelpers;

namespace MVP.Extensions
{
    public static class ModelExtensions
    {
        public static IList<Grouping<int, Contribution>> ToGroupedContributions(this IList<Contribution> list)
        {
            var result = new List<Grouping<int, Contribution>>();
            DateTime periodStart = new DateTime(DateTime.Now.Year, 4, 1);

            // If we are before the 1st of April, the period start is last year's.
            if (DateTime.Now < periodStart)
            {
                periodStart = periodStart.AddYears(-1);
            }

            var thisPeriod = list.Where(x => x.StartDate >= periodStart).OrderByDescending(x => x.StartDate);
            var lastPeriod = list.Where(x => x.StartDate < periodStart).OrderByDescending(x => x.StartDate);

            if (thisPeriod.Any())
                result.Add(new Grouping<int, Contribution>(0, thisPeriod));

            if (lastPeriod.Any())
                result.Add(new Grouping<int, Contribution>(1, lastPeriod));

            return result;
        }

        public static ContributionTypeConfig GetContributionTypeRequirements(this Guid contributionType)
        {
            var config = new ContributionTypeConfig();

            var guidString = contributionType.ToString();

            switch (guidString)
            {
                case "e36464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Article"
                    config.AnnualQuantityHeader = "Number of Articles";
                    config.AnnualReachHeader = "Number of Views";
                    config.HasAnnualReach = true;
                    break;
                case "df6464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Blog/Website Post"
                    config.AnnualQuantityHeader = "Number of Posts";
                    config.SecondAnnualQuantityHeader = "Number of Subscribers";
                    config.AnnualReachHeader = "Annual Unique Visitors";
                    config.IsUrlRequired = true;
                    config.HasAnnualReach = true;
                    config.HasSecondAnnualQuantity = true;
                    break;
                case "db6464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Book (Author)"
                    config.AnnualQuantityHeader = "Number of Books";
                    config.AnnualReachHeader = "Copies Sold";
                    config.HasAnnualReach = true;
                    break;
                case "dd6464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Book (Co-Author)"
                    config.AnnualQuantityHeader = "Number of Books";
                    config.AnnualReachHeader = "Copies Sold";
                    config.HasAnnualReach = true;
                    break;
                case "f16464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Conference (Staffing)"
                    config.AnnualQuantityHeader = "Number of Conferences";
                    config.AnnualReachHeader = "Number of Visitors";
                    config.HasAnnualReach = true;
                    break;
                case "0ce0dc15-0304-e911-8171-3863bb2bca60": // "EnglishName": "Docs.Microsoft.com Contribution"
                    config.AnnualQuantityHeader = "Pull Requests/Issues/Submissions";
                    break;
                case "f96464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Forum Moderator"
                    config.AnnualQuantityHeader = "Number of Threads moderated";
                    break;
                case "d96464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Forum Participation (3rd Party forums)"
                    config.AnnualQuantityHeader = "Number of Answers";
                    config.SecondAnnualQuantityHeader = "Number of Posts";
                    config.AnnualReachHeader = "Views of Answers";
                    config.IsUrlRequired = true;
                    config.IsSecondAnnualQuantityRequired = true;
                    config.HasAnnualReach = true;
                    config.HasSecondAnnualQuantity = true;
                    break;
                case "d76464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Forum Participation (Microsoft Forums)"
                    config.AnnualQuantityHeader = "Number of Answers";
                    config.SecondAnnualQuantityHeader = "Number of Posts";
                    config.AnnualReachHeader = "Views of Answers";
                    config.IsUrlRequired = true;
                    config.HasAnnualReach = true;
                    config.HasSecondAnnualQuantity = true;
                    break;
                case "f76464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Mentorship"
                    config.AnnualQuantityHeader = "Number of Mentorship Activity";
                    config.AnnualReachHeader = "Number of Mentees";
                    config.HasAnnualReach = true;
                    break;
                case "d2d96407-0304-e911-8171-3863bb2bca60": // "EnglishName": "Microsoft Open Source Projects"
                    config.AnnualQuantityHeader = "Number of Projects";
                    break;
                case "414bcf30-e889-e511-8110-c4346bac0abc": // "EnglishName": "Non-Microsoft Open Source Projects"
                    config.AnnualQuantityHeader = "Project(s)";
                    config.AnnualReachHeader = "Contributions";
                    config.HasAnnualReach = true;
                    break;
                case "fd6464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Organizer (User Group/Meetup/Local Events)"
                    config.AnnualQuantityHeader = "Meetings";
                    config.AnnualReachHeader = "Members";
                    config.HasAnnualReach = true;
                    break;
                case "ef6464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Organizer of Conference"
                    config.AnnualQuantityHeader = "Number of Conferences";
                    config.AnnualReachHeader = "Number of Attendees";
                    config.HasAnnualReach = true;
                    break;
                case "ff6464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Other"
                    config.AnnualQuantityHeader = "Annual Quantity";
                    config.AnnualReachHeader = "Annual Reach";
                    config.HasAnnualReach = true;
                    break;
                case "016564de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Product Group Feedback (General)"
                    config.AnnualQuantityHeader = "Number of Events Participated";
                    config.AnnualReachHeader = "Number of Feedbacks Provided";
                    config.HasAnnualReach = true;
                    break;
                case "e96464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Sample Code/Projects/Tools"
                    config.AnnualQuantityHeader = "Number of Samples";
                    config.AnnualReachHeader = "Number of Downloads";
                    config.HasAnnualReach = true;
                    config.IsUrlRequired = true;
                    break;
                case "fb6464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Site Owner"
                    config.AnnualQuantityHeader = "Number of Sites";
                    config.AnnualReachHeader = "Number of Visitors";
                    config.HasAnnualReach = true;
                    break;
                case "d16464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Speaking (Conference)"
                    config.AnnualQuantityHeader = "Number of Talks";
                    config.SecondAnnualQuantityHeader = "";
                    config.AnnualReachHeader = "Attendees of Talks";
                    break;
                case "d56464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Speaking (User Group/Meetup/Local events)"
                    config.AnnualQuantityHeader = "Number of Talks";
                    config.AnnualReachHeader = "Attendees of Talks";
                    config.HasAnnualReach = true;
                    break;
                case "eb6464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Technical Social Media (Twitter, Facebook, LinkedIn...)"
                    config.AnnualQuantityHeader = "Number of Talks";
                    config.AnnualReachHeader = "Number of Followers";
                    config.HasAnnualReach = true;
                    config.IsUrlRequired = true;
                    break;
                case "056564de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Translation Review, Feedback and Editing"
                    config.AnnualQuantityHeader = "Annual Quantity";
                    break;
                case "e56464de-179a-e411-bbc8-6c3be5a82b68": // "EnglishName": "Video/Webcast/Podcast"
                    config.AnnualQuantityHeader = "Number of Videos";
                    config.AnnualReachHeader = "Number of Views";
                    config.HasAnnualReach = true;
                    config.IsUrlRequired = true;
                    break;
                case "0ee0dc15-0304-e911-8171-3863bb2bca60": // "EnglishName": "Workshop/Volunteer/Proctor"
                    config.AnnualQuantityHeader = "Number of Events";
                    break;
            }

            return config;
        }
    }
}
