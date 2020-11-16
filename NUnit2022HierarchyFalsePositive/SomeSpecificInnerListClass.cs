using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit2022HierarchyFalsePositive
{
	class SomeSpecificInnerListClass : InnerListClass
	{
		public string LeafProperty { get; set; }
	}
}
