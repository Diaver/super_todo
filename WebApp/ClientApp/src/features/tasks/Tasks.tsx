import React, {useEffect} from 'react';
import {createStyles, makeStyles, Theme} from '@material-ui/core/styles';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemSecondaryAction from '@material-ui/core/ListItemSecondaryAction';
import ListItemText from '@material-ui/core/ListItemText';
import IconButton from '@material-ui/core/IconButton';
import CommentIcon from '@material-ui/icons/Comment';
import {Container, Divider, TextField, Typography} from "@material-ui/core";
import {useDispatch, useSelector} from "react-redux";
import {addAsync, completeAsync, deleteAsync, loadTasksByUserIdAsync, loadUsersAsync, selectedUserSelector, tasksSelector, tasksSlice, textSelector, usersSelector} from "./tasksSlice";
import DeleteIcon from '@material-ui/icons/Delete';
import Autocomplete from '@material-ui/lab/Autocomplete';
import {TextValidator, ValidatorForm} from "react-material-ui-form-validator";
import AssignmentIcon from '@material-ui/icons/Assignment';
import {ITodoTaskUserResponse} from "../../apiModels/todoTasksApi/Response/ITodoTaskUserResponse";
import {TodoTaskStatus} from "../../apiModels/common/TodoTaskStatus";
import {ITodoTaskResponse} from "../../apiModels/todoTasksApi/Response/ITodoTaskResponse";

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
        textBoxWithIconContainer: {
            display: "flex",
            alignItems: "end",
            marginTop: 32,
            "& div": {
                width: "100%"
            },
        },

        textBoxWithIcon: {
            flexGrow: 1,
            marginLeft: 8
        },
        form: {
            width: '100%', // Fix IE 11 issue.
        },
    }),
);

export function Tasks() {
    const classes = useStyles();
    const tasks = useSelector(tasksSelector);
    const users = useSelector(usersSelector);
    const text = useSelector(textSelector);
    const selectedUser = useSelector(selectedUserSelector);
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(loadUsersAsync());
    }, []);

    const onChange = async (event: any, newValue: any) => {
        await dispatch(tasksSlice.actions.setSelectedUser(newValue))
        dispatch(loadTasksByUserIdAsync())
        console.log(newValue);
    }

    return (
        <Container component="main" maxWidth="md">
            <Typography variant={"h4"}>
                <AssignmentIcon/> Tasks
            </Typography>

            <Autocomplete
                id="combo-box-demo"
                options={users}
                value={selectedUser}
                getOptionSelected={(option: ITodoTaskUserResponse, value: ITodoTaskUserResponse) => (selectedUser?.userId !== option.userId)}
                onChange={onChange}
                fullWidth
                getOptionLabel={(option) => option.name}
                style={{marginTop: 32}}
                renderInput={(params) => <TextField {...params} label="Select user" variant="outlined" fullWidth/>}
            />
            <List>
                {tasks.map((item: ITodoTaskResponse) => {
                    const labelId = `checkbox-list-label-${item.todoTaskId}`;

                    return (
                        <ListItem
                            key={item.todoTaskId}
                            dense
                            button
                            className={classes.itemAdded}
                            onClick={() => {
                                dispatch(completeAsync(item.todoTaskId))
                            }}>
                            <ListItemText id={labelId} primary={item.text} style={{textDecoration: item.status === TodoTaskStatus.completed ? 'line-through' : 'none'}}/>
                            <ListItemSecondaryAction>
                                <IconButton
                                    edge="end"
                                    aria-label="delete button"
                                    onClick={() => {
                                        dispatch(deleteAsync(item.todoTaskId))
                                    }}>
                                    <DeleteIcon/>
                                </IconButton>
                            </ListItemSecondaryAction>
                        </ListItem>
                    );
                })}
            </List>

            <Divider/>

            <ValidatorForm
                className={classes.form}
                onSubmit={() => {
                    dispatch(addAsync());
                }}
                onError={errors => console.log(errors)}
            >
                {
                    selectedUser &&
                    <div className={classes.textBoxWithIconContainer}>
                        <CommentIcon/>
                        <TextValidator
                            variant="outlined"
                            name="Text"
                            className={classes.textBoxWithIcon}
                            fullWidth={true}
                            label="New Task"
                            helperText="Please enter text for new task"
                            value={text}
                            onChange={(event: any) => dispatch(tasksSlice.actions.setText(event.target.value))}
                            validators={['required']}
                            errorMessages={["This field is required"]}

                        />
                    </div>
                }
            </ValidatorForm>

        </Container>
    );
}