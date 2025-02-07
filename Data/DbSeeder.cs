//using Bogus;
//using MyBlogApp.Models;

//namespace MyBlogApp.Data
//{
//    public static class DbSeeder
//    {
//        public static void Seed(ApplicationDbContext context)
//        {
//            // Ensure the database is created
//            context.Database.EnsureCreated();

//            // Check if data already exists
//            if (context.Users.Any() || context.Blogs.Any() || context.Comments.Any())
//            {
//                return; // Exit if data already exists
//            }

//            // ✅ Step 1: Generate Fake Users
//            var userFaker = new Faker<User>()
//                .RuleFor(u => u.UserId, f => Guid.NewGuid())
//                .RuleFor(u => u.UserName, f => f.Internet.UserName())
//                .RuleFor(u => u.UserEmail, f => f.Internet.Email())
//                .RuleFor(u => u.UserPhone, f => f.Phone.PhoneNumber())
//                .RuleFor(u => u.ProfilePicture, f => f.Internet.Avatar())
//                .RuleFor(u => u.IsAuthor, f => f.Random.Bool())
//                .RuleFor(u => u.CreatedAt, f => f.Date.Past());

//            var users = userFaker.Generate(10); // Generate 10 fake users
//            context.Users.AddRange(users);
//            context.SaveChanges(); // Save users first

//            // ✅ Step 2: Generate Fake Blogs
//            var blogFaker = new Faker<Blog>()
//                .RuleFor(b => b.BlogId, f => Guid.NewGuid())
//                .RuleFor(b => b.BlogTitle, f => f.Lorem.Sentence(5, 10))
//                .RuleFor(b => b.BlogContent, f => f.Lorem.Paragraphs(3, 6))
//                .RuleFor(b => b.BlogImage, f => f.Image.PicsumUrl())
//                .RuleFor(b => b.CreatedAt, f => f.Date.Past(1))
//                .RuleFor(b => b.AuthorId, f => f.PickRandom(users).UserId) // Ensure valid UserId
//                .RuleFor(b => b.AutherUserName, (f, b) => users.First(u => u.UserId == b.AuthorId).UserName)
//                .RuleFor(b => b.UserId, (f, b) => b.AuthorId) // Ensure foreign key consistency
//                .RuleFor(b => b.User, (f, b) => users.First(u => u.UserId == b.AuthorId));

//            var blogs = blogFaker.Generate(20); // Generate 20 fake blogs
//            context.Blogs.AddRange(blogs);
//            context.SaveChanges(); // Save blogs after users

//            // ✅ Step 3: Generate Fake Comments
//            var commentFaker = new Faker<Comment>()
//                .RuleFor(c => c.CommentId, f => Guid.NewGuid())
//                .RuleFor(c => c.CommentContent, f => f.Lorem.Sentences(2))
//                .RuleFor(c => c.CreatedAt, f => f.Date.Past(1))
//                .RuleFor(c => c.BlogId, f => f.PickRandom(blogs).BlogId) // Ensure valid BlogId
//                .RuleFor(c => c.CommentedUserId, f => f.PickRandom(users).UserId) // Ensure valid UserId
//                .RuleFor(c => c.CommentedUserName, (f, c) => users.First(u => u.UserId == c.CommentedUserId).UserName)
//                .RuleFor(c => c.Blog, (f, c) => blogs.First(b => b.BlogId == c.BlogId))
//                .RuleFor(c => c.CommentedUser, (f, c) => users.First(u => u.UserId == c.CommentedUserId));

//            var comments = commentFaker.Generate(50); // Generate 50 fake comments
//            context.Comments.AddRange(comments);
//            context.SaveChanges(); // Save comments after users and blogs
//        }
//    }
//}
