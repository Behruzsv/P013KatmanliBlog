﻿using P013KatmanliBlog.Core.Entities;

namespace P013KatmanliBlog.MVCUI.Models
{
	public class PostDetailViewModel
	{
		public Post Post { get; set; }
		public List<Post>? RelatePosts { get; set; }
		public List<Category>? Categories { get; set; }
	}
}
