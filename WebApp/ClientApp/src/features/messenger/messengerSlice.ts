import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {RootState} from '../../app/rootStore';

interface MessengerState {
    messages: any[];
    contacts: any[];
    chat?: any;
    loading: boolean;
    searchText: string
}

const initialState: MessengerState = {
    messages: [],
    contacts: [],
    loading: false,
    searchText: ""
};

export const messengerSlice = createSlice({
    name: 'messenger',
    initialState,
    reducers: {
        messagesLoaded: (state, action: PayloadAction<any[]>) => {
            state.messages = action.payload
        },
        contactsLoaded: (state, action: PayloadAction<any[]>) => {
            state.contacts = action.payload
        },
        setLoading: (state, action: PayloadAction<boolean>) => {
            state.loading = action.payload
        },
        setChat: (state, action: PayloadAction<any>) => {
            state.chat = action.payload
        },
        setSearchText: (state, action: PayloadAction<string>) => {
            state.searchText = action.payload
        },
        finish: (state) => {
            state.searchText = "";
        },
    },
});

export const messagesSelector = (state: RootState) => state.messenger.messages;
export const contactsSelector = (state: RootState) => state.messenger.contacts;
export const searchTextSelector = (state: RootState) => state.messenger.searchText;
export const chatSelector = (state: RootState) => state.messenger.chat;

export default messengerSlice.reducer;
