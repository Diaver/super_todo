import * as Cookies from 'js-cookie'
import NavigationService from "./NavigationService";

class SessionService {
    SessionGuid?: string;

    getSessionGuid() {
        return Cookies.get('SessionGuid')
    }
    setSessionGuid(sessionGuid: string, expirationDate: Date) {

        Cookies.set('SessionGuid', sessionGuid, {path: '/', expires: new Date(expirationDate)});

        this.SessionGuid = sessionGuid;
    }

    redirectToLogin() {
        if (window.location.pathname && window.location.pathname.indexOf('/login') >= 0)
            return;

        NavigationService.go('/login/');
        
    }

    removeSessionGuid() {
        Cookies.remove('SessionGuid', {path: '/'})
    }

    isUserLoggedIn() {
        return this.getSessionGuid() !== undefined
    }


}

const api = new SessionService();

export default api as SessionService;
