using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Folding;

namespace TVCStudio.SourceCodeHandling
{
    public abstract class CodeFolding : IDisposable
    {
        protected CodeFolding(TextArea textArea)
        {
            FoldingManager = FoldingManager.Install(textArea);
            m_Timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(FoldingRefreshTime) };
            m_Timer.Tick += OnTimerTick;
        }
        public void Dispose()
        {

            m_Timer.Stop();
            m_Timer.Tick -= OnTimerTick;
            if (FoldingManager != null)
            {
                FoldingManager.Uninstall(FoldingManager);
                FoldingManager = null;
            }
        }

        public void SuspendUpdate(bool clearFoldings = false)
        {
            m_Timer.Stop();
            if (clearFoldings)
            {
                FoldingManager.UpdateFoldings(new List<NewFolding>(), -1);
            }
        }

        public void ResumeUpdate()
        {
            m_Timer.Start();
        }

        public void CollapseAll()
        {
            if (FoldingManager != null)
            {
                foreach (FoldingSection folding in FoldingManager.AllFoldings)
                {
                    folding.IsFolded = true;
                }
            }
        }
        public void ExpandAll()
        {
            if (FoldingManager != null)
            {
                foreach (FoldingSection folding in FoldingManager.AllFoldings)
                {
                    folding.IsFolded = false;
                }
            }
        }

        public int FoldingCount
        {
            get
            {
                if (FoldingManager != null && FoldingManager.AllFoldings.Any())
                {
                    return FoldingManager.AllFoldings.Count();
                }

                return 0;
            }
        }

        protected abstract void UpdateFolding();

        private void OnTimerTick(object sender, EventArgs e)
        {
            UpdateFolding();
        }

        protected FoldingManager FoldingManager { get; private set; }
        private readonly DispatcherTimer m_Timer;
        private const int FoldingRefreshTime = 2;
    }
}
