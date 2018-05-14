namespace _09_Rectangle_Intersection
{
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            var firstTokens = Console.ReadLine()
                .Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var numberOfRectangles = firstTokens[0];
            var intersectionChecks = firstTokens[1];

            var rectangles = new Rectangle[numberOfRectangles];

            for (int i = 0; i < numberOfRectangles; i++)
            {
                var rectangleArgs = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var id = rectangleArgs[0];
                var width = int.Parse(rectangleArgs[1]);
                var height = int.Parse(rectangleArgs[2]);
                var horizontal = double.Parse(rectangleArgs[3]);
                var vertical = double.Parse(rectangleArgs[4]);

                var rectangle = new Rectangle(id, width, height, horizontal, vertical);
                rectangles[i] = rectangle;
            }

            for (int i = 0; i < intersectionChecks; i++)
            {
                var intersectionCheckArgs = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var firstId = intersectionCheckArgs[0];
                var secondId = intersectionCheckArgs[1];

                var firstRectangle = rectangles.First(r => r.Id == firstId);
                var secondRectangle = rectangles.First(r => r.Id == secondId);

                if (firstRectangle.DoesIntersect(secondRectangle))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }
            }
        }
    }
}