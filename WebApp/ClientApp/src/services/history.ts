import {createBrowserHistory} from "history";
export let history: any;

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
history = createBrowserHistory({ basename: baseUrl as string });

export default history;