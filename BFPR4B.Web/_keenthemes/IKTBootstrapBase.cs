using BFPR4B.Web._keenthemes.libs;

namespace BFPR4B.Web._keenthemes;

public interface IKTBootstrapBase
{
    void InitThemeMode();
    
    void InitThemeDirection();

    void InitLayout();
    
    void Init(IKTTheme theme);
}