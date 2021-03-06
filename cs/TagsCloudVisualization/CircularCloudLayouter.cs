﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WordCloudVisualization
{
    public class CircularCloudLayouter
    {
        private float spiralCounter = 0;

        public CircularCloudLayouter(Point center)
        {
            CenterPosition = center;
        }

        public Point CenterPosition { get; }
        public List<Rectangle> Layout { get; } = new List<Rectangle>();

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rect = GetNextEmptyRectangleAtSpiral(rectangleSize);
            Layout.Add(rect);
            return rect;
        }

        private Rectangle GetNextEmptyRectangleAtSpiral(Size rectangleSize)
        {
            return GetPointsOnSpiral()
                .Select(p => new Rectangle(p - rectangleSize / 2, rectangleSize))
                .SkipWhile(r => Layout.Any(r.IntersectsWith)).First();
        }

        private IEnumerable<Point> GetPointsOnSpiral()
        {
            while (true)
            {
                float distanceFromCenter = MathF.Sqrt(spiralCounter);
                int x = (int) (distanceFromCenter * MathF.Cos(spiralCounter)) + CenterPosition.X;
                int y = (int) (distanceFromCenter * MathF.Sin(spiralCounter++)) + CenterPosition.Y;
                yield return new Point(x, y);
            }
        }
    }
}