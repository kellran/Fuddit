using System;
using System.ComponentModel;
using Fuddit.Data;
using Fuddit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fuddit.Data
{
    public class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            var adminRole = new IdentityRole("Admin");
            rm.CreateAsync(adminRole).Wait();
            
            var admin = new ApplicationUser
                {UserName = "admin@uia.no", Email = "admin@uia.no", EmailConfirmed = true};
            um.CreateAsync(admin, "Password1.").Wait();
            um.AddToRoleAsync(admin, "Admin");

            var user1 = new ApplicationUser
            {
                UserName = "Chris@hotmail.com", Email = "Chris@hotmail.com", Nickname = "Kelran", EmailConfirmed = true, Bio = "Level 60 balance druid, 1400cr 0exp"
            };
            um.CreateAsync(user1, "Password1.").Wait();
            var user2 = new ApplicationUser
            {
                UserName = "Robin@hotmail.com", Email = "Robin@hotmail.com", Nickname = "Holicrash", EmailConfirmed = true, Bio = "Huge fan of videogames, some favorites include: Dark souls and Psychonauts."
            };
            um.CreateAsync(user2, "Password1.").Wait();
            var user3 = new ApplicationUser
            {
                UserName = "Benji@hotmail.com", Email = "Benji@hotmail.com", Nickname = "Benji", EmailConfirmed = true
            };
            um.CreateAsync(user3, "Password1.").Wait();
            var user4 = new ApplicationUser
            {
                UserName = "Amund@hotmail.com", Email = "Amund@hotmail.com", Nickname = "Amunski", EmailConfirmed = true
            };
            um.CreateAsync(user4, "Password1.").Wait();
            var user5 = new ApplicationUser
            {
                UserName = "Adrian@hotmail.com", Email = "Adrian@hotmail.com", Nickname = "Rockhooie", EmailConfirmed = true
            };
            um.CreateAsync(user5, "Password1.").Wait();


            db.Posts.Add(new Post
            {

                Category = "None",
                Title = "Why paladins are the ultimate burst machines",
                Body = "If you stack massive amounts of mastery on a paladin and go with these talents: Executioner's sentence and Final reckoning." +
                       "Afterwards join the 'Kyrian' covenant and go for the upgrade to divine toll. Stacking all of these bonuses together with 5 holy power you are able to do this combo:" +
                       "pop seraphim, avenging wrath, Final reckoning, Hammer og justice and finally Executioners sentence. The just go all out pumping as much damage as possible. If successfull you will deal (depending on gear) around 40k damage over a very small period of time, and that is NOT including the raw damage pumped in just to get the execute.",
                Tags = "World of warcraft, Paladins, One-shot, #Fuck druids, gaming",
                User = user2
            });
            db.Posts.Add(new Post
            {
                Category = "None",
                Title = "Lorem Ipsum",
                Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dapibus luctus velit, ut lacinia dolor sollicitudin a. Phasellus condimentum augue lacus, ut blandit metus accumsan sit amet. Mauris vulputate blandit quam, quis imperdiet risus placerat ut. Lorem ipsum dolor sit amet, consectetur adipiscing elit. In convallis, lectus eget accumsan semper, tortor ante fermentum nibh, quis cursus lacus leo eget est. In hac habitasse platea dictumst. In fermentum libero metus, eget fermentum dolor mattis vel. Ut ultrices sollicitudin facilisis. Etiam eu vulputate ligula. Vivamus euismod quam eget suscipit fringilla. Praesent a venenatis libero. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nulla at erat eu justo tincidunt eleifend et id mi. Integer ut augue et lectus porttitor luctus. Etiam tempor rhoncus sem, quis euismod nulla ornare at. Quisque molestie aliquam mi, vitae scelerisque felis viverra vel. Nunc eros dui, hendrerit id mollis non, euismod in libero. Quisque congue leo eget eleifend mattis. Vivamus fermentum aliquet odio non aliquet. Duis sit amet erat mattis, semper enim sit amet, ultrices turpis. Nulla lobortis odio ac metus iaculis condimentum. Quisque gravida mauris non auctor consectetur. Pellentesque eget condimentum elit. Donec facilisis sed diam quis vehicula. Suspendisse commodo vestibulum diam, nec tempor massa efficitur ut. Nam blandit erat non placerat tempor. Fusce efficitur fermentum nisl sed maximus. Quisque sagittis facilisis varius. Aenean in egestas enim. Etiam ut rutrum quam. Vivamus eget est felis. Proin non gravida justo. Sed efficitur magna a eros condimentum auctor. Curabitur sem diam, interdum eget sapien ut, ultricies ornare sapien. Pellentesque ornare purus sit amet luctus viverra. Mauris nec varius nibh. In ac erat scelerisque, pretium justo et, varius nunc. Etiam in augue luctus, hendrerit neque non, pretium odio. Pellentesque nec faucibus justo, et tempus eros. Curabitur nec tincidunt lectus, vitae pretium orci. Donec libero orci, suscipit sit amet mi eu, imperdiet vestibulum massa. Aenean consectetur interdum lorem et blandit. In consequat nulla enim, bibendum gravida orci faucibus consequat. Nulla interdum mi id interdum semper. Pellentesque vel gravida ipsum, id porttitor felis. Donec mollis odio non nisi varius molestie. Mauris lacinia libero et ligula semper efficitur. Praesent et ligula quis sapien pharetra sodales at ut magna. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Proin tempor mi velit, posuere bibendum dolor tincidunt vitae. Quisque varius leo risus, quis pulvinar justo ullamcorper sit amet.",
                Tags = "Lorem, Ipsum, Loremipsum, latin, Testtext",
                User = user3
            });
            db.Posts.Add(new Post
            {
                Category = "None",
                Title = "Am i pregent?",
                Body = "This other day i read on a forum that prigant people feel sick often, i have been feeling sick so im wondering if i might be preganenant!",
                Tags = "what, funny", 
                User = user1
            });
            db.Posts.Add(new Post
            {
                Category = "None",
                Title = "LOOKING SELL LEAGUE ACCOUNT",
                Body = "Hey, I would love to sell my legendary 100% fiat currency union, I spent more than 100 dollars, if you are interested in this, I think the best is, thank you very much.",
                Tags = "100%, LOL, gaming", 
                User = user1
            });
            db.Posts.Add(new Post
            {
                Category = "None",
                Title = "Titles are hard",
                Body = "INSERT MEME HERE, i mean comon i feel like at least 50% of reddit and shit like that is JUST MEMES. but i cant upload pictures yet so here we are.",
                Tags = "Feck, peck, leck, heck, deck, snek",
                User = user4
            });
            db.Posts.Add(new Post
            {
                Category = "None",
                Title = "Please stay inside dont be dumb",
                Body = "Covid is still rampaging around the world and people are still out and about, its completely retarded, why dont people stay inside instead? Inside has food, computers and comfort.", 
                Tags = "peoplearedumb, covid-19, corona, news",
                User = user5

            });
            db.Posts.Add(new Post
            {
                Category = "None",
                Title = "The dictator speech",
                Body = "I’m sorry, but I don’t want to be an emperor. That’s not my business. I don’t want to rule or conquer anyone. I should like to help everyone - if possible - Jew, Gentile - black man - white. We all want to help one another. Human beings are like that. We want to live by each other’s happiness - not by each other’s misery. We don’t want to hate and despise one another. In this world there is room for everyone. And the good earth is rich and can provide for everyone. The way of life can be free and beautiful, but we have lost the way." +
                       "Greed has poisoned men’s souls, has barricaded the world with hate, has goose-stepped us into misery and bloodshed. We have developed speed, but we have shut ourselves in. Machinery that gives abundance has left us in want. Our knowledge has made us cynical. Our cleverness, hard and unkind. We think too much and feel too little. More than machinery we need humanity. More than cleverness we need kindness and gentleness. Without these qualities, life will be violent and all will be lost…." +
                       "The aeroplane and the radio have brought us closer together. The very nature of these inventions cries out for the goodness in men - cries out for universal brotherhood - for the unity of us all. Even now my voice is reaching millions throughout the world - millions of despairing men, women, and little children - victims of a system that makes men torture and imprison innocent people." +
                       "To those who can hear me, I say - do not despair. The misery that is now upon us is but the passing of greed - the bitterness of men who fear the way of human progress. The hate of men will pass, and dictators die, and the power they took from the people will return to the people. And so long as men die, liberty will never perish. ….." +
                       "Soldiers! don’t give yourselves to brutes - men who despise you - enslave you - who regiment your lives - tell you what to do - what to think and what to feel! Who drill you - diet you - treat you like cattle, use you as cannon fodder. Don’t give yourselves to these unnatural men - machine men with machine minds and machine hearts! You are not machines! You are not cattle! You are men! You have the love of humanity in your hearts! You don’t hate! Only the unloved hate - the unloved and the unnatural! Soldiers! Don’t fight for slavery! Fight for liberty!" +
                       "In the 17th Chapter of St Luke it is written: “the Kingdom of God is within man” - not one man nor a group of men, but in all men! In you! You, the people have the power - the power to create machines. The power to create happiness! You, the people, have the power to make this life free and beautiful, to make this life a wonderful adventure." +
                       "Then - in the name of democracy - let us use that power - let us all unite. Let us fight for a new world - a decent world that will give men a chance to work - that will give youth a future and old age a security. By the promise of these things, brutes have risen to power. But they lie! They do not fulfil that promise. They never will!" +
                       "Dictators free themselves but they enslave the people! Now let us fight to fulfil that promise! Let us fight to free the world - to do away with national barriers - to do away with greed, with hate and intolerance. Let us fight for a world of reason, a world where science and progress will lead to all men’s happiness. Soldiers! in the name of democracy, let us all unite!",
                Tags = "Thedictator, CharlieChaplin, Speech",
                User = user3
            });
            
            db.Posts.Add(new Post
            {
                Category = "None",
                Title = "How do i play Warlocks in D&D?",
                Body = "Hey i recently picked up D&D starter edition and i really want to try a warlock but im having trouble understanding how their spellcasting works. do i always cast all spells at level 5, that seems way to OP for a level 4 party. please resond",
                Tags = "Warlock, D&D, Wizardsofthecoast, help, gaming", 
                User = user5
            });
            
            
            db.SaveChanges();
        }
    }
}