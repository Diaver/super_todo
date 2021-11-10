import * as React from 'react';
import {IContactResponse} from "../../apiModels/contactsApi/Response/IContactResponse";
import {Avatar, ListItem, ListItemAvatar, ListItemText} from "@mui/material";

export function Contact(data: IContactResponse) {

    return (
        <ListItem button>
            <ListItemAvatar>
                <Avatar alt="Profile Picture"/>
            </ListItemAvatar>
            <ListItemText primary={data.name}/>
        </ListItem>
    );
}