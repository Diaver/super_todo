import history from "./history";

class NavigationService {

    pastLocations: string[] = [];

    goBack() {

        if (this.pastLocations.length > 1) {
            this.pastLocations.pop();
        }

        if (history.length === 1) {
            history.push("/");
        } else {
            history.goBack();
        }
    }

    getPrev() {
        if (this.pastLocations.length <= 1) {
            return "/";
        } else {
            this.pastLocations.pop(); // remove current location;
            
            return this.pastLocations[this.pastLocations.length - 1];
        }
    }

    go(url: string) {
        this.pastLocations.push(url);
        history.push(url);
    }
}

const api = new NavigationService();

export default api as NavigationService;
