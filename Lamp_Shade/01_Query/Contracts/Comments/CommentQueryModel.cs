using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Contracts.Comments
{
	public class CommentQueryModel
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Message { get; set; }
		public long ProductId { get; set; }
	}
}
