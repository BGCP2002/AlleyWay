using UnityEngine;
using UnityEngine.UI;

namespace J_Func.UI
{
    public class FlexibleLayout : LayoutGroup
    {
        enum FitType
        {
            Square,
            FixedWidth,
            FixedHeight,
            Height,
            Width,
        }
        [SerializeField] private FitType fitType;
        [SerializeField, Min(1)] private int width = 1;
        [SerializeField, Min(1)] private int height = 1;
        [SerializeField, Min(0)] private Vector2 spacing;

        private Vector2 cellSize;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            int childCount = transform.childCount;

            // Calculate grid size
            int maxRows = 1;
            int maxCols = 1;

            int sqrt = Mathf.CeilToInt(Mathf.Sqrt(childCount));
            maxRows = sqrt;
            maxCols = sqrt;

            switch (fitType)
            {
                case FitType.Square:
                    break;
                case FitType.FixedWidth:
                    maxCols = height;
                    maxRows = Mathf.CeilToInt(childCount / (float)height);
                    break;
                case FitType.FixedHeight:
                    maxRows = width;
                    maxCols = Mathf.CeilToInt(childCount / (float)width);
                    break;
                case FitType.Width:
                    maxRows = Mathf.CeilToInt(childCount / (float)maxCols);
                    break;
                case FitType.Height:
                    maxCols = Mathf.CeilToInt(childCount / (float)maxRows);
                    break;
            }

            // Validate ( Avoid "divide by zero" )
            maxCols = Mathf.Max(maxCols, 1);
            maxRows = Mathf.Max(maxRows, 1);

            // Calculate cell size
            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            float totalSpacingX = spacing.x * (maxCols - 1);
            float totalSpacingY = spacing.y * (maxRows - 1);

            float cellWidth = (parentWidth - totalSpacingX) / maxCols;
            float cellHeight = (parentHeight - totalSpacingY) / maxRows;

            cellSize = new Vector2(cellWidth, cellHeight);

            // Asign cell size and position
            for (int i = 0; i < rectChildren.Count; i++)
            {
                var child = rectChildren[i];

                int row = i / maxCols;
                int col = i % maxCols;

                float xPos = cellSize.x * col;
                float yPos = cellSize.y * row;

                xPos += spacing.x * col;
                yPos += spacing.y * row;

                SetChildAlongAxis(child, 0, xPos, cellSize.x);
                SetChildAlongAxis(child, 1, yPos, cellSize.y);
            }
        }

        public override void CalculateLayoutInputVertical()
        {

        }

        public override void SetLayoutHorizontal()
        {

        }

        public override void SetLayoutVertical()
        {

        }
    }
}