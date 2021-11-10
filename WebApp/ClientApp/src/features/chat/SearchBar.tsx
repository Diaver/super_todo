import * as React from 'react';
import {InputAdornment, TextField} from "@mui/material";
import {Search} from "@material-ui/icons";
import {useDispatch} from "react-redux";
import {chatSlice} from "./chatSlice";

export function SearchBar() {
    const dispatch = useDispatch();

    return (
        <TextField id="standard-basic" label="Search for contacts" variant="standard"
                   fullWidth
                   onChange={(event: any) => dispatch(chatSlice.actions.setSearchText(event.target.value))}
                   InputProps={{
                       endAdornment: (
                           <InputAdornment position="end">
                               <Search/>
                           </InputAdornment>
                       )
                   }}/>
    );
}