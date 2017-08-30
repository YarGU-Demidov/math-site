namespace MathSite.ViewModels.Capability
{
	public class PostPreviewViewModel
	{
		public PostPreviewViewModel(string title, string url, string content, string publishedAt, string previewImage = null)
		{
			Title = title;
			Url = url;
			PreviewImage = previewImage;
			Content = content;
			PublishedAt = publishedAt;
		}

		public string Title { get; }
		public string Url { get; }
		public string PreviewImage { get; }
		public string Content { get; }
		public string PublishedAt { get; }
	}
}