namespace Wayfarer.Mvc.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;
    using Wayfarer.Mvc.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Wayfarer.Mvc.Models.WayfarerContext>
    {
        public Configuration()
        {
           // AutomaticMigrationsEnabled = false;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Wayfarer.Mvc.Models.WayfarerContext context)
        {

            /*Establish database connection*/
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            /*Delete junk*/
            context.Database.ExecuteSqlCommand("DELETE Status");
            context.Database.ExecuteSqlCommand("DELETE Friendships");

            /*Configure roles*/
            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            #region Create Administrator
            if (!WebSecurity.UserExists("tinc2k"))
                WebSecurity.CreateUserAndAccount("tinc2k", "tinc2k", new { Email = "tin@0bit.co", Phone = "+385915773004", Disabled = false });
 
            if (!Roles.GetRolesForUser("tinc2k").Contains("Administrator"))
                Roles.AddUsersToRoles(new[] {"tinc2k"}, new[] {"Administrator"});
            #endregion
            #region Give the Admin some personality
            var user = context.UserProfiles.First(u => u.UserName == "tinc2k");
            context.UserProfileRegions.AddOrUpdate(
                upr => new { upr.Name, upr.Value },
                new UserProfileRegion()
                {
                    Audience = Audience.Public,
                    User = user,
                    Name = "Favourite authors",
                    Value = "William Burroughs, Hunter Thompson, Jack Kerouac, Allen Ginsberg, Kurt Vonnegut"
                },
                new UserProfileRegion()
                {
                    Audience = Audience.Public,
                    User = user,
                    Name = "Favourite films",
                    Value = "Apocalypse Now, The Godfather, Fight Club, American Beauty, Trainspotting"
                }
            );
            #endregion

            /*Create test users and give them some data and relatioships*/

            #region Create User Monroe
            if (!WebSecurity.UserExists("monroe"))
                WebSecurity.CreateUserAndAccount("monroe", "monroe", new { Email = "monroe@monroe.org", Phone = "+2223333444", Disabled = false });
            user = context.UserProfiles.First(u => u.UserName == "monroe");
            context.UserProfileRegions.AddOrUpdate(
                upr => new { upr.Name, upr.Value },
                new UserProfileRegion()
                {
                    Audience = Audience.Public,
                    User = user,
                    Name = "Awesome music",
                    Value = "Depeche Mode, Daft Punk, Kraftwerk, Florence + The Machine, nipplepeople, Pretty Lights"
                },
                new UserProfileRegion()
                {
                    Audience = Audience.Friends,
                    User = user,
                    Name = "Interested in",
                    Value = "Men, Women"
                }
            );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public,
                     Author = user,
                     TimePosted = DateTime.UtcNow.AddHours(-2).AddMinutes(11),
                     TimeLastEdited = DateTime.UtcNow.AddHours(-2).AddMinutes(37),
                     Content = "Some of you are really smart. You know who you are. Some of you are really thick. Unfortunately, you don't know who you are."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public,
                     Author = user,
                     TimePosted = DateTime.UtcNow.AddHours(-1).AddMinutes(25),
                     TimeLastEdited = DateTime.UtcNow.AddHours(-1).AddMinutes(30),
                     Content = "It's going to be a night of partying and heavy drinking. Or as Charlie Sheen calls it: Breakfast."
                 }
             );

            context.Statuses.AddOrUpdate(
                new Status
                {
                    Audience = Audience.Public,
                    Author = user,
                    TimePosted = DateTime.UtcNow,
                    TimeLastEdited = DateTime.UtcNow,
                    Content = "\"Angelheaded hipsters burning for the ancient heavenly connection to the starry dynamo in the machinery of night.\" - Allen Ginsberg"
                }
            );
            for (var i = 0; i < 100; i++)
            {
                context.Statuses.AddOrUpdate(
                    new Status
                    {
                        Audience = Audience.Public,
                        Author = user,
                        TimePosted = DateTime.UtcNow.AddDays(-200).AddDays(i),
                        TimeLastEdited = DateTime.UtcNow.AddDays(-100).AddDays(i),
                        Content = "Monroe's status #" + i.ToString() + "."
                    }
                );
            }
            #endregion

            #region Create User Elon Musk
            if (!WebSecurity.UserExists("elon"))
                WebSecurity.CreateUserAndAccount("elon", "elon", new { Email = "elon@teslamotors.com", Phone = "+1112222333", Disabled = false });
            user = context.UserProfiles.First(u => u.UserName == "elon");
            context.UserProfileRegions.AddOrUpdate(
                upr => new { upr.Name, upr.Value },
                new UserProfileRegion()
                {
                    Audience = Audience.Public,
                    User = user,
                    Name = "Place of Birth",
                    Value = "Pretoria, South Africa"
                },
                new UserProfileRegion()
                {
                    Audience = Audience.Public,
                    User = user,
                    Name = "Founded",
                    Value = "X.Com, PayPal, SpaceX, Tesla Motors, Hyperloop"
                }
            );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-1).AddHours(-1), TimeLastEdited = DateTime.UtcNow.AddDays(-1).AddHours(-1),
                     Content = "Failure is an option here. If things are not failing, you are not innovating enough."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-2).AddHours(-2), TimeLastEdited = DateTime.UtcNow.AddDays(-2).AddHours(-2),
                     Content = "There's a fundamental difference, if you look into the future, between a humanity that is a space-faring civilization, that's out there exploring the stars... compared with one where we are forever confined to Earth until some eventual extinction event."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-3).AddHours(-3), TimeLastEdited = DateTime.UtcNow.AddDays(-3).AddHours(-3),
                     Content = "We have essentially no patents in SpaceX. Our primary long-term competition is in China. If we published patents, it would be farcical, because the Chinese would just use them as a recipe book."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-4).AddHours(-4), TimeLastEdited = DateTime.UtcNow.AddDays(-4).AddHours(-4),
                     Content = "Physics is a good framework for thinking. … Boil things down to their fundamental truths and reason up from there."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-5).AddHours(-5), TimeLastEdited = DateTime.UtcNow.AddDays(-5).AddHours(-5),
                     Content = "My biggest mistake is probably weighing too much on someone's talent and not someone's personality. I think it matters whether someone has a good heart."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-6).AddHours(-6), TimeLastEdited = DateTime.UtcNow.AddDays(-6).AddHours(-6),
                     Content = "If something is important enough, even if the odds are against you, you should still do it."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-7).AddHours(-7), TimeLastEdited = DateTime.UtcNow.AddDays(-7).AddHours(-7),
                     Content = "The first step is to establish that something is possible; then probability will occur."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-8).AddHours(-8), TimeLastEdited = DateTime.UtcNow.AddDays(-8).AddHours(-8),
                     Content = "It is remarkable how many things you can explode. I’m lucky I have all my fingers."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-9).AddHours(-9), TimeLastEdited = DateTime.UtcNow.AddDays(-9).AddHours(-9),
                     Content = "You want to be extra rigorous about making the best possible thing you can. Find everything that’s wrong with it and fix it. Seek negative feedback, particularly from friends"
                 }
             );
            #endregion

            #region Create User Charles Bukowski
            if (!WebSecurity.UserExists("bukowski"))
                WebSecurity.CreateUserAndAccount("bukowski", "bukowski", new { Email = "bukowski@deadpoets.org", Phone = "+46728927813", Disabled = false });
            user = context.UserProfiles.First(u => u.UserName == "bukowski");
            context.UserProfileRegions.AddOrUpdate(
                upr => new { upr.Name, upr.Value },
                new UserProfileRegion()
                {
                    Audience = Audience.Public, User = user,
                    Name = "My novels",
                    Value = "Post Office, Factotum, Women, Ham on Rye, Hollywood, Pulp"
                }
            );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-1).AddHours(-1), TimeLastEdited = DateTime.UtcNow.AddDays(-1).AddHours(-1),
                     Content = "Some people never go crazy. What truly horrible lives they must lead."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-2).AddHours(-2), TimeLastEdited = DateTime.UtcNow.AddDays(-2).AddHours(-2),
                     Content = "Sometimes you climb out of bed in the morning and you think, I'm not going to make it, but you laugh inside — remembering all the times you've felt that way."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-3).AddHours(-3), TimeLastEdited = DateTime.UtcNow.AddDays(-3).AddHours(-3),
                     Content = "My ambition is handicapped by laziness."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-4).AddHours(-4), TimeLastEdited = DateTime.UtcNow.AddDays(-4).AddHours(-4),
                     Content = "We're all going to die, all of us, what a circus! That alone should make us love each other but it doesn't. We are terrorized and flattened by trivialities, we are eaten up by nothing."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-5).AddHours(-5), TimeLastEdited = DateTime.UtcNow.AddDays(-5).AddHours(-5),
                     Content = "You have to die a few times before you can really live."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-6).AddHours(-6), TimeLastEdited = DateTime.UtcNow.AddDays(-6).AddHours(-6),
                     Content = "I don't hate people. I just feel better when they aren't around."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-7).AddHours(-7), TimeLastEdited = DateTime.UtcNow.AddDays(-7).AddHours(-7),
                     Content = "An intellectual says a simple thing in a hard way. An artist says a hard thing in a simple way."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-8).AddHours(-8), TimeLastEdited = DateTime.UtcNow.AddDays(-8).AddHours(-8),
                     Content = "I loved you like a man loves a woman he never touches, only writes to, keeps little photographs of."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-9).AddHours(-9), TimeLastEdited = DateTime.UtcNow.AddDays(-9).AddHours(-9),
                     Content = "If you're losing your soul and you know it, then you've still got a soul left to lose."
                 }
             );
            #endregion

            #region Create User Hunter Thompson
            if (!WebSecurity.UserExists("hunter"))
                WebSecurity.CreateUserAndAccount("hunter", "hunter", new { Email = "hunter@deadpoets.org", Phone = "+943665432763", Disabled = false });
            user = context.UserProfiles.First(u => u.UserName == "hunter");
            context.UserProfileRegions.AddOrUpdate(
                upr => new { upr.Name, upr.Value },
                new UserProfileRegion()
                {
                    Audience = Audience.Public, User = user,
                    Name = "My books",
                    Value = "Hell's Angels: The Strange and Terrible Saga of the Outlaw Motorcycle Gangs, Fear and Loathing in Las Vegas, Fear and Loathing on the Campaign Trail '72, Gonzo Papers Vol. 1-4, The Curse of Lono, The Rum Diary, Screw-Jack"
                }
            );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-1).AddHours(-1), TimeLastEdited = DateTime.UtcNow.AddDays(-1).AddHours(-1),
                     Content = "In a closed society where everybody's guilty, the only crime is getting caught. In a world of thieves, the only final sin is stupidity."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-2).AddHours(-2), TimeLastEdited = DateTime.UtcNow.AddDays(-2).AddHours(-2),
                     Content = "America... just a nation of two hundred million used car salesmen with all the money we need to buy guns and no qualms about killing anybody else in the world who tries to make us uncomfortable."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-3).AddHours(-3), TimeLastEdited = DateTime.UtcNow.AddDays(-3).AddHours(-3),
                     Content = "If you're going to be crazy, you have to get paid for it or else you're going to be locked up."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-4).AddHours(-4), TimeLastEdited = DateTime.UtcNow.AddDays(-4).AddHours(-4),
                     Content = "You better take care of me Lord, if you don't you're gonna have me on your hands."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-5).AddHours(-5), TimeLastEdited = DateTime.UtcNow.AddDays(-5).AddHours(-5),
                     Content = "There is nothing more helpless and irresponsible than a man in the depths of an ether binge."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-6).AddHours(-6), TimeLastEdited = DateTime.UtcNow.AddDays(-6).AddHours(-6),
                     Content = "I have a theory that the truth is never told during the nine-to-five hours."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-7).AddHours(-7), TimeLastEdited = DateTime.UtcNow.AddDays(-7).AddHours(-7),
                     Content = "A word to the wise is infuriating."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-8).AddHours(-8), TimeLastEdited = DateTime.UtcNow.AddDays(-8).AddHours(-8),
                     Content = "For every moment of triumph, for every instance of beauty, many souls must be trampled."
                 }
             );
            context.Statuses.AddOrUpdate(
                 new Status
                 {
                     Audience = Audience.Public, Author = user,
                     TimePosted = DateTime.UtcNow.AddDays(-9).AddHours(-9), TimeLastEdited = DateTime.UtcNow.AddDays(-9).AddHours(-9),
                     Content = "Going to trial with a lawyer who considers your whole life-style a Crime in Progress is not a happy prospect."
                 }
             );
            #endregion
            
            #region Friendships
            var tinc2k = context.UserProfiles.First(up => up.UserName == "tinc2k");
            var monroe = context.UserProfiles.First(up => up.UserName == "monroe");
            var elon = context.UserProfiles.First(up => up.UserName == "elon");
            var bukowski = context.UserProfiles.First(up => up.UserName == "bukowski");
            context.Friendships.AddOrUpdate(
                new Friendship() { Profile = tinc2k, Friend = elon },
                new Friendship() { Profile = tinc2k, Friend = bukowski },
                new Friendship() { Profile = tinc2k, Friend = monroe },
                new Friendship() { Profile = monroe, Friend = tinc2k }
                );
            #endregion

            #region Create New Dummy Users (tinc3k, tinc4k)
            if (!WebSecurity.UserExists("tinc3k"))
                WebSecurity.CreateUserAndAccount("tinc3k", "tinc3k", new { Email = "tinc3k@0bit.co", Phone = "+4865411563", Disabled = false });
            if (!WebSecurity.UserExists("tinc4k"))
                WebSecurity.CreateUserAndAccount("tinc4k", "tinc4k", new { Email = "tinc4k@0bit.co", Phone = "+1824186486", Disabled = false });
            #endregion

        }

    }
}
