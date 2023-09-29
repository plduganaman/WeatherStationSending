using System;
using System.ComponentModel;
using System.Diagnostics;

namespace WeatherStationSending.My
{
    internal static partial class MyProject
    {
        internal partial class MyForms
        {

            [EditorBrowsable(EditorBrowsableState.Never)]
            public frmOptions m_frmOptions;

            public frmOptions frmOptions
            {
                [DebuggerHidden]
                get
                {
                    m_frmOptions = Create__Instance__(m_frmOptions);
                    return m_frmOptions;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_frmOptions))
                        return;
                    if (value is not null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_frmOptions);
                }
            }


            [EditorBrowsable(EditorBrowsableState.Never)]
            public ShutDown m_ShutDown;

            public ShutDown ShutDown
            {
                [DebuggerHidden]
                get
                {
                    m_ShutDown = Create__Instance__(m_ShutDown);
                    return m_ShutDown;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_ShutDown))
                        return;
                    if (value is not null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_ShutDown);
                }
            }

        }


    }
}