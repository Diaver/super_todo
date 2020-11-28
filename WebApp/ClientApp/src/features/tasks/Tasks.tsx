import React from 'react';
import {createStyles, makeStyles, Theme} from '@material-ui/core/styles';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemSecondaryAction from '@material-ui/core/ListItemSecondaryAction';
import ListItemText from '@material-ui/core/ListItemText';
import Checkbox from '@material-ui/core/Checkbox';
import IconButton from '@material-ui/core/IconButton';
import CommentIcon from '@material-ui/icons/Comment';
import {Container, TextField} from "@material-ui/core";
import {useDispatch, useSelector} from "react-redux";
import {itemsSelector, tasksSlice} from "./tasksSlice";
import InputAdornment from '@material-ui/core/InputAdornment';
import DeleteIcon from '@material-ui/icons/Delete';

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
    const dispatch = useDispatch();
    const [checked, setChecked] = React.useState([]);
    const [newItemText, setNewItemText] = React.useState<string>("");

    const handleToggle = (value: string) => () => {
           // const currentIndex = checked.indexOf(value);
            const newChecked = [...checked];
    
            /*if (currentIndex === -1) {
                newChecked.push(value);
            } else {
                newChecked.splice(currentIndex, 1);
            }*/
    
            setChecked(newChecked);
    };

    return (
        <Container component="main" maxWidth="md">
            <List>
                {items.map((item) => {
                    const labelId = `checkbox-list-label-${item.taskId}`;

                    return (
                        <ListItem key={item.taskId} role={undefined} dense button onClick={handleToggle(item.taskId)} className={classes.itemAdded}>
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
                                <IconButton edge="end" aria-label="comments" onClick={()=> dispatch(tasksSlice.actions.deleteItem(item.taskId))}>
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
                        dispatch(tasksSlice.actions.addItem(newItemText));
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