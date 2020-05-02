using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace XamlCompletionExample
{
    [Export(typeof(ICompletionSourceProvider))]
    [ContentType("xaml")]
    [Order(After = "default")]
    [Name("XAML Intellisense Limitation Example")]
    public class ExampleXamlCompletionSourceProvider : ICompletionSourceProvider
    {
        [Import]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }

        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer)
        {
            return new ExampleXamlCompletionSource(this, textBuffer);
        }
    }

}
