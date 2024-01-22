using System;
namespace OrderListManagerApi3.Translate
{
	public class ListDto
	{
		public List<GroupDto> Groups { get; set; }
		

		public ListDto()
		{
			Groups = new List<GroupDto>();
			
		}
	}
}

