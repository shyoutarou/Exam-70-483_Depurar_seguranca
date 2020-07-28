using System.Security;

namespace CasWriterDemo
{
//[SecurityCritical()]
public class CasWriterInheritance : CasWriter
{
    [SecurityCritical()]
    public override void WriteCustomSentence(string text)
    {
        base.WriteCustomSentence(text);
    }
}
}
