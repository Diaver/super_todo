import {toast} from "react-toastify";
import {IApiResultBase} from "../apiModels/common/IApiResult";

class NotificationService {

    onPromiseRejected(error: any) {
        if (NotificationService.tryHandlePromiseRejectedError(error)) {
            return;
        }

        console.log("Error:" + error);

        toast.error(error, {
            position: toast.POSITION.TOP_CENTER,  autoClose: 5000
        });
    }

    static tryHandlePromiseRejectedError(error: any) {
      /*  if (error.response && error.response.status === 401) {
            SessionService.redirectToLogin();
            return true;
        }*/

        return false;
    }
    
    onRequestFailed(response: IApiResultBase, autoClose: false | number = false) {
        toast.error(response.message, {
            position: toast.POSITION.TOP_CENTER, autoClose: 3000
        });
    }



    onErrorMessage(message: string) {
        toast.error(message, {
            position: toast.POSITION.TOP_RIGHT, autoClose: 3000
        });
    }

    onSuccessMessage(message: string) {
        toast.success(message, {
            position: toast.POSITION.TOP_RIGHT, autoClose: 3000
        });
    }
}

const api = new NotificationService();
export default api as NotificationService;


