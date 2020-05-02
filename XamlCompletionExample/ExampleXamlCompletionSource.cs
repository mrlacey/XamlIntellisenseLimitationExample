using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace XamlCompletionExample
{
    public class ExampleXamlCompletionSource : ICompletionSource
    {
        private bool _isDisposed;
        private readonly ExampleXamlCompletionSourceProvider sourceProvider;
        private readonly ITextBuffer textBuffer;

        public ExampleXamlCompletionSource(ExampleXamlCompletionSourceProvider sourceProvider, ITextBuffer textBuffer)
        {
            this.sourceProvider = sourceProvider;
            this.textBuffer = textBuffer;
        }

        public void AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
        {
            if (_isDisposed) return;

            var currentPoint = (session.TextView.Caret.Position.BufferPosition) - 1;
            var navigator = this.sourceProvider.NavigatorService.GetTextStructureNavigator(this.textBuffer);
            var extent = navigator.GetExtentOfWord(currentPoint);
            var currentlyEnteredSpan = currentPoint.Snapshot.CreateTrackingSpan(extent.Span, SpanTrackingMode.EdgeInclusive);

            // A simple example of adding something
            // Obviously, in a real implementation this would look at various factors to produce appropriate entries.
            var mySuggestion = new Completion("My Suggestion", "{SomethingAmazing}", string.Empty, null, string.Empty);

            // Can't add to what the XamlLanguageService provides (as it's readonly)
            // So create own set to display option(s)
            var completions = new ObservableCollection<Completion>
            {
                mySuggestion
            };

            CompletionSet set = new CompletionSet("Demo", "Demo", currentlyEnteredSpan, completions, Enumerable.Empty<Completion>());
            completionSets.Add(set);
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            GC.SuppressFinalize(this);
            _isDisposed = true;
        }
    }
}
