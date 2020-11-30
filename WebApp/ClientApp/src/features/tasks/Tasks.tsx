import React, {useEffect} from 'react';
import {createStyles, makeStyles, Theme} from '@material-ui/core/styles';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemSecondaryAction from '@material-ui/core/ListItemSecondaryAction';
import ListItemText from '@material-ui/core/ListItemText';
import Checkbox from '@material-ui/core/Checkbox';
import IconButton from '@material-ui/core/IconButton';
import CommentIcon from '@material-ui/icons/Comment';
import {Container, TextField, Typography} from "@material-ui/core";
import {useDispatch, useSelector} from "react-redux";
import {itemsSelector, loadUsersAsync, tasksSlice, usersSelector} from "./tasksSlice";
import InputAdornment from '@material-ui/core/InputAdornment';
import DeleteIcon from '@material-ui/icons/Delete';
import Autocomplete from '@material-ui/lab/Autocomplete';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {},
        itemAdded: {
            animation: `$createBox 0.5s`,
        },
        '@keyframes createBox': {
            "0%": {
                transform: "scale(0.2)"
            },
            "100%": {
                transform: "scale(1)"
            }
        },
    }),
);

export function Tasks() {
    const classes = useStyles();
    const items = useSelector(itemsSelector);
    const users = useSelector(usersSelector);
    const dispatch = useDispatch();
    const [newItemText, setNewItemText] = React.useState<string>("");

    useEffect(() => {
        dispatch(loadUsersAsync());
    }, []);


    return (
        <Container component="main" maxWidth="md">
            <Typography variant={"h4"}>
                Tasks
            </Typography>
            <Autocomplete
                id="combo-box-demo"
                options={users}
                getOptionLabel={(option) => option.name}
                style={{ width: 300 }}
                renderInput={(params) => <TextField {...params} label="Select user" variant="outlined" />}
            />
            <List>
                {items.map((item) => {
                    const labelId = `checkbox-list-label-${item.todoTaskId}`;

                    return (
                        <ListItem key={item.todoTaskId} role={undefined} dense button className={classes.itemAdded}>
                            <ListItemIcon>
                                <Checkbox
                                    edge="start"
                                    tabIndex={-1}
                                    disableRipple
                                    inputProps={{'aria-labelledby': labelId}}
                                />
                            </ListItemIcon> 
                            <ListItemText id={labelId}  primary={item.text}/>
                            <ListItemSecondaryAction >
                                <IconButton edge="end" aria-label="comments" onClick={()=> {}}>
                                    <DeleteIcon/>
                                </IconButton>
                            </ListItemSecondaryAction>
                        </ListItem>
                    );
                })}
            </List>
            <TextField
                fullWidth
                label="New Task"
                helperText="Please enter text for new task"
                value={newItemText}
                onChange={event => setNewItemText(event.target.value)}
                onKeyPress={ev => {
                    if (ev.key === 'Enter') {
                       /* dispatch(tasksSlice.actions.addItem(newItemText));*/
                        setNewItemText("");
                    }
                }}
                InputProps={{
                    startAdornment: (
                        <InputAdornment position="start">
                            <CommentIcon/>
                        </InputAdornment>
                    ),
                }}
            />
        </Container>
    );
}