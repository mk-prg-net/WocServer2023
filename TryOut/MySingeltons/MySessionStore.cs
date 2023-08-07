using System.Collections.Concurrent;

namespace TryOut.MySingeltons
{
    /// <summary>
    /// mko, 6.8.2023
    /// Simpler Sitzungsspeicher
    /// </summary>
    public class MySessionStore
    {
        /// <summary>
        /// mko, 6.8.2023
        /// Einfacher Sitzungszustand
        /// </summary>
        public class Session
        {
            public Session(long SessionId, string UserName) 
            {
                this.SessionId = SessionId;
                this.UserName = UserName;
            }

            public long SessionId;

            public string UserName { get; set; }

            public ConcurrentDictionary<string, string> SessionState = new ConcurrentDictionary<string, string>();
        }

        /// <summary>
        /// Liste der aktuell aktiven Sitzungszustände
        /// </summary>
        ConcurrentDictionary<long, Session> sessionStore = new ConcurrentDictionary<long, Session>();

        /// <summary>
        /// Berechnet threadsafe eine neue Sitzungsnummer
        /// </summary>
        /// <returns></returns>
        //public long NewSessionId() =>  Interlocked.Increment(ref _lastSessionId);
        //static long _lastSessionId;
        public long NewSessionId() => MKPRG.GUID64.GUID64Generator.NewGUID64();

        /// <summary>
        /// Erzeugt eine neue Sitzung für einen Benutzer
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public Session CreateNewSession(string UserName)
        {
            var session = new Session(NewSessionId(), UserName);
            sessionStore[session.SessionId] = session;
            return session;
        }

        /// <summary>
        /// Beendet eine Sitzung
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool FinishSession(long sessionId)
        {
            if(sessionStore.ContainsKey(sessionId))
            {
                return sessionStore.TryRemove(sessionId, out _);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Holt eine vorhandene Sitzung
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public async Task<(bool SessionFound, Session? session)> GetSession(long sessionId)
        {
            if (sessionStore.ContainsKey(sessionId))
            {
                return await Task.FromResult((true, sessionStore[sessionId]));
            }
            else
            {
                return await Task.FromResult((false, (Session)null));
            }
        }


        public async Task<(bool SessionFound, Session? session)> GetSessionFor(string UserName)
        {
            if (sessionStore.Values.Any(r => r.UserName == UserName))
            {
                return await Task.FromResult((true, sessionStore.Values.First(r => r.UserName == UserName)));
            }
            else
            {
                return await Task.FromResult((false, (Session)null));
            }
        }

    }
}
