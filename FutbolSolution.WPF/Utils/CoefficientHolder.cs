namespace FutbolSolution.WPF.Utils
{
    public class CoefficientHolder
    {
        public static decimal Intercept { get; set; } = -1.5m;
        public static decimal BetaTeamStrength { get; set; } = 0.7m;
        public static decimal BetaTeamStats { get; set; } = 0.4m;
        public static decimal BetaRefBias {  get; set; } = 0.2m;
        public static decimal BetaMatchHistory { get; set; } = 0.3m;
    }
}
