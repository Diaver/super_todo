import * as React from 'react';
import {useDispatch, useSelector} from "react-redux";
import {Avatar, List, ListItem, ListItemAvatar, ListItemText, ListSubheader} from "@mui/material";
import {chatSlice, contactsSelector, loadContactsAsync, searchTextSelector} from "./chatSlice";
import {useEffect} from "react";

export function AllContacts() {
    const contacts = useSelector(contactsSelector);
    const searchText = useSelector(searchTextSelector);
    const dispatch = useDispatch();
    
    useEffect(() => {
        dispatch(loadContactsAsync());
    }, []);
    
    useEffect( () => {
        dispatch(chatSlice.actions.setFilteredContacts(searchText));
    }, [searchText])

    return (
        <List>
            <ListSubheader sx={{bgcolor: 'background.paper'}} disableSticky={true}>
                All contacts
            </ListSubheader>
            {contacts.map(({contactId, name}) => (
                <React.Fragment key={contactId}>
                    <ListItem button>
                        <ListItemAvatar>
                            <Avatar alt="Profile Picture"/>
                        </ListItemAvatar>
                        <ListItemText primary={name}/>
                    </ListItem>
                </React.Fragment>
            ))}
        </List>
    );
}