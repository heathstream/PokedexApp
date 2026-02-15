namespace PokedexApp.Controls
{
    public partial class ShimmerView : ContentView
    {
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(
                nameof(CornerRadius),
                typeof(float),
                typeof(ShimmerView),
                0f);

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public ShimmerView()
        {
            InitializeComponent();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent != null)
                StartShimmer();
        }

        void StartShimmer()
        {
            var animation = new Animation(v =>
            {
                var brush = (LinearGradientBrush)ShimmerBox.Background;
                brush.GradientStops[0].Offset = (float)v - 1;
                brush.GradientStops[1].Offset = (float)v;
                brush.GradientStops[2].Offset = (float)v + 0.5f;
            }, -1.0, 2.0);

            animation.Commit(this, "Shimmer", length: 800, easing: Easing.Linear, repeat: () => true);
        }
    }
}
