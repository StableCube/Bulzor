using System;
using System.Text;

namespace StableCube.Bulzor.Components
{
    public class BulmaClassBuilder
    {
        private StringBuilder _sb = new StringBuilder();
        private string _baseClass;
        private BulColor? _color;
        private BulSchemeColor? _schemeColor;
        private BulSize? _size;
        private BulSize? _sizeChild;
        private BulColor? _textColor;
        private BulColor? _backgroundColor;
        private BulTextSize? _textSize;
        private BulTextWeight? _textWeight;
        private BulColumnSize? _colSize;
        private BulSeparator? _separator;
        private BulRatio? _ratio;
        private BulDimension? _dimension;
        private bool? _isRounded;
        private bool? _isHoverable;
        private bool? _isHovered;
        private bool? _isFocused;
        private bool? _isActive;
        private bool? _isLoading;
        private bool? _isStatic;
        private bool? _hasIconsLeft;
        private bool? _hasIconsRight;
        private bool? _isBoxed;
        private bool? _isToggle;
        private bool? _isToggleRounded;
        private bool? _hasAddons;
        private bool? _isCentered;
        private bool? _hasDropdown;
        private bool? _hasDropdownUp;
        private bool? _isArrowless;
        private bool? _isRight;
        private bool? _isLeft;
        private bool? _isMultiple;
        private bool? _isSelected;
        private bool? _isSpaced;
        private bool? _isLight;
        private bool? _isOutlined;
        private bool? _isFullWidth;
        private bool? _isInverted;
        private bool? _hasName;
        private bool? _isCircle;
        private bool? _hasRatio;
        private bool? _isDelete;
        private bool? _isVertical;
        private bool? _isUp;

        public BulmaClassBuilder(){}

        public BulmaClassBuilder(string baseClass)
        {
            _baseClass = baseClass;
        }

        public void SetColor(BulColor? value)
        {
            if(value.HasValue && value.Value == BulColor.Default)
                value = null;
            
            _color = value;
        }

        public void SetSchemeColor(BulSchemeColor? value)
        {
            if(value.HasValue && value.Value == BulSchemeColor.Default)
                value = null;
            
            _schemeColor = value;
        }

        public void SetSize(BulSize? value)
        {
            if(value.HasValue && value.Value == BulSize.Default)
                value = null;

            _size = value;
        }

        public void SetSizeChild(BulSize? value)
        {
            if(value.HasValue && value.Value == BulSize.Default)
                value = null;
            
            _sizeChild = value;
        }

        public void SetTextColor(BulColor? value)
        {
            if(value.HasValue && value.Value == BulColor.Default)
                value = null;

            _textColor = value;
        }

        public void SetTextSize(BulTextSize? value)
        {
            if(value.HasValue && value.Value == BulTextSize.Default)
                value = null;

            _textSize = value;
        }

        public void SetTextWeight(BulTextWeight? value)
        {
            if(value.HasValue && value.Value == BulTextWeight.Default)
                value = null;
            
            _textWeight = value;
        }

        public void SetBackgroundColor(BulColor? value)
        {
            if(value.HasValue && value.Value == BulColor.Default)
                value = null;

            _backgroundColor = value;
        }

        public void SetColumnSize(BulColumnSize? value)
        {
            if(value.HasValue && value.Value == BulColumnSize.Default)
                value = null;

            _colSize = value;
        }

        public void SetSeparator(BulSeparator? value)
        {
            if(value.HasValue && value.Value == BulSeparator.Default)
                value = null;
            
            _separator = value;
        }

        public void SetRatio(BulRatio? value)
        {
            if(value.HasValue && value.Value == BulRatio.Default)
                value = null;
            
            _ratio = value;
        }

        public void SetDimension(BulDimension? value)
        {
            if(value.HasValue && value.Value == BulDimension.Default)
                value = null;
            
            _dimension = value;
        }

        public void SetIsRounded(bool? value)
        {
            _isRounded = value;
        }

        public void SetIsHoverable(bool? value)
        {
            _isHoverable = value;
        }

        public void SetIsHovered(bool? value)
        {
            _isHovered = value;
        }

        public void SetIsFocused(bool? value)
        {
            _isFocused = value;
        }

        public void SetIsActive(bool? value)
        {
            _isActive = value;
        }

        public void SetIsLoading(bool? value)
        {
            _isLoading = value;
        }

        public void SetHasIconsLeft(bool? value)
        {
            _hasIconsLeft = value;
        }

        public void SetHasIconsRight(bool? value)
        {
            _hasIconsRight = value;
        }

        public void SetIsStatic(bool? value)
        {
            _isStatic = value;
        }

        public void SetIsBoxed(bool? value)
        {
            _isBoxed = value;
        }

        public void SetIsToggle(bool? value)
        {
            _isToggle = value;
        }

        public void SetIsToggleRounded(bool? value)
        {
            _isToggleRounded = value;
        }

        public void SetHasAddons(bool? value)
        {
            _hasAddons = value;
        }

        public void SetIsCentered(bool? value)
        {
            _isCentered = value;
        }

        public void SetHasDropdown(bool? value)
        {
            _hasDropdown = value;
        }

        public void SetHasDropdownUp(bool? value)
        {
            _hasDropdownUp = value;
        }

        public void SetIsArrowless(bool? value)
        {
            _isArrowless = value;
        }

        public void SetIsLeft(bool? value)
        {
            _isLeft = value;
        }

        public void SetIsRight(bool? value)
        {
            _isRight = value;
        }

        public void SetIsMultiple(bool? value)
        {
            _isMultiple = value;
        }

        public void SetIsSelected(bool? value)
        {
            _isSelected = value;
        }

        public void SetIsSpaced(bool? value)
        {
            _isSpaced = value;
        }

        public void SetIsLight(bool? value)
        {
            _isLight = value;
        }

        public void SetIsOutlined(bool? value)
        {
            _isOutlined = value;
        }

        public void SetIsFullWidth(bool? value)
        {
            _isFullWidth = value;
        }

        public void SetIsInverted(bool? value)
        {
            _isInverted = value;
        }

        public void SetHasName(bool? value)
        {
            _hasName = value;
        }

        public void SetIsCircle(bool? value)
        {
            _isCircle = value;
        }

        public void SetHasRatio(bool? value)
        {
            _hasRatio = value;
        }

        public void SetIsDelete(bool? value)
        {
            _isDelete = value;
        }

        public void SetIsVertical(bool? value)
        {
            _isVertical = value;
        }

        public void SetIsUp(bool? value)
        {
            _isUp = value;
        }

        public override string ToString()
        {
            _sb.Clear();
            
            if(_baseClass != null)
                Append(_baseClass);

            if(_color.HasValue)
            {
                Append(BulmaVariableMap.Color(_color.Value));
            }

            if(_schemeColor.HasValue)
            {
                Append(BulmaVariableMap.SchemeColor(_schemeColor.Value));
            }

            if(_size.HasValue)
            {
                Append(BulmaVariableMap.Size(_size.Value));
            }

            if(_sizeChild.HasValue)
            {
                Append(BulmaVariableMap.ChildSize(_sizeChild.Value));
            }
            
            if(_textColor.HasValue)
            {
                Append(BulmaVariableMap.TextColor(_textColor.Value));
            }

            if(_backgroundColor.HasValue)
            {
                Append(BulmaVariableMap.BackgroundColor(_backgroundColor.Value));
            }
            
            if(_textSize.HasValue)
            {
                Append(BulmaVariableMap.TextSize(_textSize.Value));
            }
            
            if(_textWeight.HasValue)
            {
                Append(BulmaVariableMap.TextWeight(_textWeight.Value));
            }

            if(_colSize.HasValue)
            {
                Append(BulmaVariableMap.ColumnSize(_colSize.Value));
            }

            if(_ratio.HasValue)
            {
                Append(BulmaVariableMap.Ratio(_ratio.Value));
            }

            if(_dimension.HasValue)
            {
                Append(BulmaVariableMap.Dimension(_dimension.Value));
            }

            if(_separator.HasValue)
                Append($"has-{_separator.Value.ToString().ToLower()}-separator");

            if(_isRounded.HasValue && _isRounded.Value == true)
                Append("is-rounded");

            if(_isHoverable.HasValue && _isHoverable.Value == true)
                Append("is-hoverable");

            if(_isHovered.HasValue && _isHovered.Value == true)
                Append("is-hovered");

            if(_isFocused.HasValue && _isFocused.Value == true)
                Append("is-focused");

            if(_isActive.HasValue && _isActive.Value == true)
                Append("is-active");
            
            if(_isLoading.HasValue && _isLoading.Value == true)
                Append("is-loading");

            if(_hasIconsLeft.HasValue && _hasIconsLeft.Value == true)
                Append("has-icons-left");

            if(_hasIconsRight.HasValue && _hasIconsRight.Value == true)
                Append("has-icons-right");

            if(_isStatic.HasValue && _isStatic.Value == true)
                Append("is-static");

            if(_isBoxed.HasValue && _isBoxed.Value == true)
                Append("is-boxed");

            if(_isToggle.HasValue && _isToggle.Value == true)
                Append("is-toggle");

            if(_isToggleRounded.HasValue && _isToggleRounded.Value == true)
                Append("is-toggle-rounded");

            if(_hasAddons.HasValue && _hasAddons.Value == true)
                Append("has-addons");

            if(_isCentered.HasValue && _isCentered.Value == true)
                Append("is-centered");

            if(_hasDropdown.HasValue && _hasDropdown.Value == true)
                Append("has-dropdown");

            if(_isArrowless.HasValue && _isArrowless.Value == true)
                Append("is-arrowless");

            if(_hasDropdownUp.HasValue && _hasDropdownUp.Value == true)
                Append("has-dropdown-up");

            if(_isLeft.HasValue && _isLeft.Value == true)
                Append("is-left");

            if(_isRight.HasValue && _isRight.Value == true)
                Append("is-right");

            if(_isMultiple.HasValue && _isMultiple.Value == true)
                Append("is-multiple");

            if(_isSelected.HasValue && _isSelected.Value == true)
                Append("is-selected");

            if(_isSpaced.HasValue && _isSpaced.Value == true)
                Append("is-spaced");

            if(_isLight.HasValue && _isLight.Value == true)
                Append("is-light");

            if(_isOutlined.HasValue && _isOutlined.Value == true)
                Append("is-outlined");

            if(_isFullWidth.HasValue && _isFullWidth.Value == true)
                Append("is-fullwidth");

            if(_isInverted.HasValue && _isInverted.Value == true)
                Append("is-inverted");

            if(_hasName.HasValue && _hasName.Value == true)
                Append("has-name");

            if(_isCircle.HasValue && _isCircle.Value == true)
                Append("is-circle");
            
            if(_hasRatio.HasValue && _hasRatio.Value == true)
                Append("has-ratio");
            
            if(_isDelete.HasValue && _isDelete.Value == true)
                Append("is-delete");

            if(_isVertical.HasValue && _isVertical.Value == true)
                Append("is-vertical");

            if(_isUp.HasValue && _isUp.Value == true)
                Append("is-up");

            return _sb.ToString().Trim();
        }

        private void Append(string value)
        {
            _sb.Append(" ");
            if(!string.IsNullOrEmpty(CssConfig.Prefix))
                _sb.Append(CssConfig.Prefix);

            _sb.Append(value);
        }
    }
}