using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace NUnit2022HierarchyFalsePositive
{
	class TheTest
	{
		[Test]
		public void ThisIsGoodNoNUnit2022ViolationFound()
		{
			// arrange
			SomeClass someClass = CreateData();

			// assert
			var tempWithInnerListClassesExtracted = someClass.OtherClasses.SelectMany(x => x.InnerListClasses).OfType<SomeSpecificInnerListClass>().ToList();
			Assert.That(
				tempWithInnerListClassesExtracted,
				Has.Some.Property(nameof(SomeSpecificInnerListClass.LeafProperty)));
		}

		[Test]
		public void ThisIsAlsoGoodNoNUnit2022ViolationFound()
		{
			// arrange
			SomeClass someClass = CreateData();

			// assert
			var tempWithInnerListClassesExtracted = someClass.OtherClasses.SelectMany(x => x.InnerListClasses).ToList();
			Assert.That(
				tempWithInnerListClassesExtracted,
				Has.Some.Property(nameof(SomeSpecificInnerListClass.LeafProperty)));
		}

		[Test]
		public void ThisIsFoundAsNUnit2022ViolationIfThePragmaIsRemoved()
		{
			// arrange
			SomeClass someClass = CreateData();

			// assert
			Assert.That(
				someClass.OtherClasses,
#pragma warning disable NUnit2022 // Missing property required for constraint.
				Has.Some.Property(nameof(OtherClass.InnerListClasses))
				.Some.TypeOf<SomeSpecificInnerListClass>()
				.And.Property(nameof(SomeSpecificInnerListClass.LeafProperty)));
#pragma warning restore NUnit2022 // Missing property required for constraint.
		}

		[Test]
		public void ThisIsAnotherNUnit2022ViolationThePragmaIsRemoved()
		{
			// arrange
			SomeClass someClass = CreateData();

			// assert
			var tempWithInnerListClassesExtracted = someClass.OtherClasses.SelectMany(x => x.InnerListClasses).ToList();
			Assert.That(
				tempWithInnerListClassesExtracted,
#pragma warning disable NUnit2022 // Missing property required for constraint.
				Has.Some.TypeOf<SomeSpecificInnerListClass>()
				.And.Property(nameof(SomeSpecificInnerListClass.LeafProperty)));
#pragma warning restore NUnit2022 // Missing property required for constraint.
		}

		private static SomeClass CreateData()
		{
			return new SomeClass
			{
				OtherClasses = new List<OtherClass>
				{
					new OtherClass
					{
						InnerListClasses = new List<InnerListClass>
						{
							new SomeSpecificInnerListClass
							{
								LeafProperty = "hello"
							}
						}
					}
				}
			};
		}
	}
}
