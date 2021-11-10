import * as React from 'react';
import {useDispatch, useSelector} from "react-redux";
import {List, ListSubheader} from "@mui/material";
import {chatSlice, contactsSelector, loadContactsAsync, searchTextSelector} from "./chatSlice";
import {useEffect} from "react";
import {Contact} from "./Contact";

export function Contacts() {
    const contacts = useSelector(contactsSelector);
    const searchText = useSelector(searchTextSelector);
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(loadContactsAsync());
    }, []);

    useEffect(() => {
        dispatch(chatSlice.actions.setFilteredContacts(searchText));
    }, [searchText]);

    return (
        <List>
            <ListSubheader sx={{bgcolor: 'background.paper'}} disableSticky={true}>
                All contacts
            </ListSubheader>
            {contacts.map(({contactId, name}) => (
                <Contact key={contactId} contactId={contactId} name={name}/>
            ))}
        </List>
    );
}