import { combineReducers } from "redux";

// Front
import LayoutReducer from "./layouts/reducer";

// login
import LoginReducer from "./auth/login/reducer";

// register
import RegisterReducer from "./auth/register/reducer";

// userProfile
import ProfileReducer from "./auth/profile/reducer";

// Chat
import ChatReducer from "./chat/reducer";

// MailBox
import MailboxReducer from "./mailbox/reducer";

// Calendar
import CalendarReducer from "./calendar/reducer";

// Ecommerce
import EcommerceReducer from "./ecommerce/reducer";

// HR Managment
import HRManagmentReducer from "./hrManagement/reducer";

//WMS Managment
import WMSManagmentReducer from "./planmanagement/reducer"
import LOCATIONWMSManagmentReducer from "./LocationManagement/reducer";
import DOCManagmentReducer from "./documentManagement/reducer";
// Notes
import NotesReducer from "./notes/reducer";

// Social
import SocialReducer from "./social/reducer";

// Invoice
import InvoiceReducer from "./invoice/reducer"

// Users
import UsersReducer from "./users/reducer";

const rootReducer = combineReducers({
    Layout: LayoutReducer,
    Login: LoginReducer,
    Register: RegisterReducer,
    Profile: ProfileReducer,
    Chat: ChatReducer,
    Mailbox: MailboxReducer,
    Calendar: CalendarReducer,
    Ecommerce: EcommerceReducer,
    HRManagment: HRManagmentReducer,
    Notes: NotesReducer,
    Social: SocialReducer,
    Invoice: InvoiceReducer,
    Users: UsersReducer,
    WMSManagment : WMSManagmentReducer,
    LOCATIONWMSManagment: LOCATIONWMSManagmentReducer,
    DOCManagment: DOCManagmentReducer,
});


export type RootState = ReturnType<typeof rootReducer>;

export default rootReducer;