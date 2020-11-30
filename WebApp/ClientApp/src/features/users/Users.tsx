import React, {useEffect} from 'react';
import {createStyles, makeStyles, Theme} from '@material-ui/core/styles';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import {Container, Fab, Grid, Typography} from "@material-ui/core";
import {useDispatch, useSelector} from "react-redux";
import {loadUsersAsync, usersSelector} from "./usersSlice";
import AddIcon from '@material-ui/icons/Add';
import {NavLink as RouterLink} from "react-router-dom";
import {AccountCircle} from "@material-ui/icons";

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
        fab: {
            position: 'absolute',
            bottom: theme.spacing(2),
            right: theme.spacing(2),
        },
    }),
);

export function Users() {
    const classes = useStyles();
    const users = useSelector(usersSelector);
    const dispatch = useDispatch();


    useEffect(() => {
        dispatch(loadUsersAsync());
    }, []);

    return (
        <Container component="main" maxWidth="md">
            <Grid
                container
                spacing={3}
                direction="row"
                justify="space-between"
                alignItems="center">
                <Grid item>
                    <Typography variant={"h4"}>
                        Users
                    </Typography>
                </Grid>
                <Grid item>
                    <Grid
                        spacing={1}
                        direction="row"
                        container>
                        <Grid item>
                            <Fab color="primary" aria-label="add" component={RouterLink} to="/users/add">
                                <AddIcon/>
                            </Fab>
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>

            <List>
                {users.map((user) => {
                    const labelId = `checkbox-list-label-${user.userId}`;

                    return (
                        <ListItem key={user.userId} role={undefined} dense button className={classes.itemAdded}  component={RouterLink} to={`/user/edit/${user.userId}`}>
                            <ListItemIcon>
                                <AccountCircle/>
                            </ListItemIcon>
                            <ListItemText id={labelId} primary={user.name}/>
                        </ListItem>
                    );
                })}
            </List>

        </Container>
    );
}