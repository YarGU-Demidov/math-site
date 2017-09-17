using System.Collections.Generic;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
  public class PostSeoSettingsSeeder : AbstractSeeder<PostSeoSetting>
  {
    /// <inheritdoc />
    public PostSeoSettingsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
    {
    }

    /// <inheritdoc />
    public override string SeedingObjectName { get; } = nameof(PostSeoSetting);

    /// <inheritdoc />
    protected override void SeedData()
    {
      var postSeoSettings = new[]
      {
        CreatePostSeoSettings(
          "first-url",
          "first title",
          "first description"
        ),
        CreatePostSeoSettings(
          "second-url",
          "second title",
          "second description"
        ),
        CreatePostSeoSettings(
          "third-url",
          "third title",
          "third description"
        ),
        CreatePostSeoSettings(
          "fourth-url",
          "fourth title",
          "fourth description"
        ),
        CreatePostSeoSettings(
          "fifth-url",
          "fifth title",
          "fifth description"
        ),
        CreatePostSeoSettings(
          "sixth-url",
          "sixth title",
          "sixth description"
        ),
        CreatePostSeoSettings(
          "seventh-url",
          "seventh title",
          "seventh description"
        ),
        CreatePostSeoSettings(
          "eighth-url",
          "eighth title",
          "eighth description"
        ),
        CreatePostSeoSettings(
          "ninth-url",
          "ninth title",
          "ninth description"
        ),
        CreatePostSeoSettings(
          "tenth-url",
          "tenth title",
          "tenth description"
        ),
        CreatePostSeoSettings(
          "static-page-url",
          "static page title",
          "static page description"
        ),
        CreatePostSeoSettings(
          "for-entrants",
          "for entrants title",
          "for entrants description"
        ),
        CreatePostSeoSettings(
          "for-students",
          "for students title",
          "for students description"
        ),
        CreatePostSeoSettings(
          "for-scholars",
          "for scholars title",
          "for scholars description"
        ),
        CreatePostSeoSettings(
          "contacts",
          "contacts title",
          "contacts description"
        ),
        CreatePostSeoSettings(
          "departments",
          "departments title",
          "departments description"
        ),
        CreatePostSeoSettings(
          "departments/general-math",
          "general-math title",
          "general-math description"
        ),
        CreatePostSeoSettings(
          "departments/calculus",
          "calculus title",
          "calculus description"
        ),
        CreatePostSeoSettings(
          "departments/computer-security",
          "computer-security title",
          "computer-security description"
        ),
        CreatePostSeoSettings(
          "departments/algebra",
          "algebra title",
          "algebra description"
        ),
        CreatePostSeoSettings(
          "departments/mathmod",
          "mathmod title",
          "mathmod description"
        ),
        CreatePostSeoSettings(
          "departments/differential-equations",
          "differential-equations title",
          "differential-equations description"
        ),
        CreatePostSeoSettings(
          "how-to-enter",
          "how-to-enter title",
          "how-to-enter description"
        ),
        CreatePostSeoSettings(
          "how-to-learn",
          "how-to-learn title",
          "how-to-learn description"
        ),
        CreatePostSeoSettings(
          "where-to-work",
          "where-to-work title",
          "where-to-work description"
        ),
      };

      Context.PostSeoSettings.AddRange(postSeoSettings);
    }

    private static PostSeoSetting CreatePostSeoSettings(string url, string title, string description)
    {
      return new PostSeoSetting
      {
        Url = url,
        Title = title,
        Description = description,
        PostKeywords = new List<PostKeyword>()
      };
    }
  }
}