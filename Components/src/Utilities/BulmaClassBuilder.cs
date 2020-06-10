using System;
using System.Text;

namespace StableCube.Bulzor.Components
{
    public class BulmaClassBuilder
    {
        private StringBuilder _sb = new StringBuilder();
        private string _baseClass;
        private BulPrimaryColor? _primaryColor;
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

        public BulmaClassBuilder(){}

        public BulmaClassBuilder(string baseClass)
        {
            _baseClass = baseClass;
        }

        public void SetPrimaryColor(BulPrimaryColor? value)
        {
            _primaryColor = value;
        }

        public void SetSize(BulSize? value)
        {
            _size = value;
        }

        public void SetSizeChild(BulSize? value)
        {
            _sizeChild = value;
        }

        public void SetTextColor(BulColor? value)
        {
            _textColor = value;
        }

        public void SetTextSize(BulTextSize? value)
        {
            _textSize = value;
        }

        public void SetTextWeight(BulTextWeight? value)
        {
            _textWeight = value;
        }

        public void SetBackgroundColor(BulColor? value)
        {
            _backgroundColor = value;
        }

        public void SetColumnSize(BulColumnSize? value)
        {
            _colSize = value;
        }

        public void SetSeparator(BulSeparator? value)
        {
            _separator = value;
        }

        public void SetRatio(BulRatio? value)
        {
            _ratio = value;
        }

        public void SetDimension(BulDimension? value)
        {
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

        public override string ToString()
        {
            _sb.Clear();
            
            if(_baseClass != null)
                _sb.Append(_baseClass);

            if(_primaryColor.HasValue)
            {
                _sb.Append(" ");
                _sb.Append(BulmaVariableMap.PrimaryColor(_primaryColor.Value));
            }

            if(_size.HasValue)
            {
                _sb.Append(" ");
                _sb.Append(BulmaVariableMap.Size(_size.Value));
            }

            if(_sizeChild.HasValue)
            {
                _sb.Append(" ");
                _sb.Append(BulmaVariableMap.ChildSize(_sizeChild.Value));
            }
            
            if(_textColor.HasValue)
            {
                _sb.Append(" ");
                _sb.Append(BulmaVariableMap.TextColor(_textColor.Value));
            }

            if(_backgroundColor.HasValue)
            {
                _sb.Append(" ");
                _sb.Append(BulmaVariableMap.BackgroundColor(_backgroundColor.Value));
            }
            
            if(_textSize.HasValue)
            {
                _sb.Append(" ");
                _sb.Append(BulmaVariableMap.TextSize(_textSize.Value));
            }
            
            if(_textWeight.HasValue)
            {
                _sb.Append(" ");
                _sb.Append(BulmaVariableMap.TextWeight(_textWeight.Value));
            }

            if(_colSize.HasValue)
            {
                _sb.Append(" ");
                _sb.Append(BulmaVariableMap.ColumnSize(_colSize.Value));
            }

            if(_ratio.HasValue)
            {
                _sb.Append(" ");
                _sb.Append(BulmaVariableMap.Ratio(_ratio.Value));
            }

            if(_dimension.HasValue)
            {
                _sb.Append(" ");
                _sb.Append(BulmaVariableMap.Dimension(_dimension.Value));
            }

            if(_separator.HasValue)
                _sb.Append($" has-{_separator.Value.ToString().ToLower()}-separator");

            if(_isRounded.HasValue && _isRounded.Value == true)
                _sb.Append(" is-rounded");

            if(_isHoverable.HasValue && _isHoverable.Value == true)
                _sb.Append(" is-hoverable");

            if(_isHovered.HasValue && _isHovered.Value == true)
                _sb.Append(" is-hovered");

            if(_isFocused.HasValue && _isFocused.Value == true)
                _sb.Append(" is-focused");

            if(_isActive.HasValue && _isActive.Value == true)
                _sb.Append(" is-active");
            
            if(_isLoading.HasValue && _isLoading.Value == true)
                _sb.Append(" is-loading");

            if(_hasIconsLeft.HasValue && _hasIconsLeft.Value == true)
                _sb.Append(" has-icons-left");

            if(_hasIconsRight.HasValue && _hasIconsRight.Value == true)
                _sb.Append(" has-icons-right");

            if(_isStatic.HasValue && _isStatic.Value == true)
                _sb.Append(" is-static");

            if(_isBoxed.HasValue && _isBoxed.Value == true)
                _sb.Append(" is-boxed");

            if(_isToggle.HasValue && _isToggle.Value == true)
                _sb.Append(" is-toggle");

            if(_isToggleRounded.HasValue && _isToggleRounded.Value == true)
                _sb.Append(" is-toggle-rounded");

            if(_hasAddons.HasValue && _hasAddons.Value == true)
                _sb.Append(" has-addons");

            if(_isCentered.HasValue && _isCentered.Value == true)
                _sb.Append(" is-centered");

            if(_hasDropdown.HasValue && _hasDropdown.Value == true)
                _sb.Append(" has-dropdown");

            if(_isArrowless.HasValue && _isArrowless.Value == true)
                _sb.Append(" is-arrowless");

            if(_hasDropdownUp.HasValue && _hasDropdownUp.Value == true)
                _sb.Append(" has-dropdown-up");

            if(_isLeft.HasValue && _isLeft.Value == true)
                _sb.Append(" is-left");

            if(_isRight.HasValue && _isRight.Value == true)
                _sb.Append(" is-right");

            if(_isMultiple.HasValue && _isMultiple.Value == true)
                _sb.Append(" is-multiple");

            if(_isSelected.HasValue && _isSelected.Value == true)
                _sb.Append(" is-selected");

            if(_isSpaced.HasValue && _isSpaced.Value == true)
                _sb.Append(" is-spaced");

            if(_isLight.HasValue && _isLight.Value == true)
                _sb.Append(" is-light");

            if(_isOutlined.HasValue && _isOutlined.Value == true)
                _sb.Append(" is-outlined");

            if(_isFullWidth.HasValue && _isFullWidth.Value == true)
                _sb.Append(" is-fullwidth");

            if(_isInverted.HasValue && _isInverted.Value == true)
                _sb.Append(" is-inverted");

            if(_hasName.HasValue && _hasName.Value == true)
                _sb.Append(" has-name");

            if(_isCircle.HasValue && _isCircle.Value == true)
                _sb.Append(" is-circle");
            
            return _sb.ToString();
        }
    }
}