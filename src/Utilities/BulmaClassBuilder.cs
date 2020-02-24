using System;
using System.Text;

namespace StableCube.Bulzor
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
        private bool? _isRounded;
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

        public BulmaClassBuilder()
        {
        }

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

        public void SetIsRounded(bool? value)
        {
            _isRounded = value;
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

        public override string ToString()
        {
            _sb.Clear();
            
            if(_baseClass != null)
                _sb.Append(_baseClass);

            if(_primaryColor.HasValue)
                _sb.Append(" " + BulmaVariableMap.PrimaryColor(_primaryColor.Value));

            if(_size.HasValue)
                _sb.Append(" " + BulmaVariableMap.Size(_size.Value));

            if(_sizeChild.HasValue)
                _sb.Append(" " + BulmaVariableMap.ChildSize(_sizeChild.Value));

            if(_textColor.HasValue)
                _sb.Append(" " + BulmaVariableMap.TextColor(_textColor.Value));

            if(_backgroundColor.HasValue)
                _sb.Append(" " + BulmaVariableMap.BackgroundColor(_backgroundColor.Value));

            if(_textSize.HasValue)
                _sb.Append(" " + BulmaVariableMap.TextSize(_textSize.Value));

            if(_textWeight.HasValue)
                _sb.Append(" " + BulmaVariableMap.TextWeight(_textWeight.Value));

            if(_colSize.HasValue)
                _sb.Append(" " + BulmaVariableMap.ColumnSize(_colSize.Value));

            if(_isRounded.HasValue && _isRounded.Value == true)
                _sb.Append(" is-rounded");

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

            return _sb.ToString();
        }
    }
}