import React, {PropsWithChildren} from 'react';
import clsx from 'clsx';
import {createStyles, makeStyles, useTheme, Theme} from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';
import CssBaseline from '@material-ui/core/CssBaseline';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import {Box, Button, Menu, MenuItem} from "@material-ui/core";
import AssignmentIcon from '@material-ui/icons/Assignment';
import PeopleIcon from '@material-ui/icons/People';
import {NavLink as RouterLink} from "react-router-dom";
import {Copyright} from "./Copyright";
import AccountCircle from '@material-ui/icons/AccountCircle';
import SessionService from "../../services/SessionService";
import {useDispatch, useSelector} from "react-redux";
import {currentUserSelector, rootLayoutSlice} from "../rootLayout/rootLayoutSlice";
import NavigationService from "../../services/NavigationService";
import {Chat} from "@material-ui/icons";

const drawerWidth = 240;

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            display: 'flex',
        },
        appBar: {
            zIndex: theme.zIndex.drawer + 1,
            transition: theme.transitions.create(['width', 'margin'], {
                easing: theme.transitions.easing.sharp,
                duration: theme.transitions.duration.leavingScreen,
            }),
        },
        appBarShift: {
            marginLeft: drawerWidth,
            width: `calc(100% - ${drawerWidth}px)`,
            transition: theme.transitions.create(['width', 'margin'], {
                easing: theme.transitions.easing.sharp,
                duration: theme.transitions.duration.enteringScreen,
            }),
        },
        menuButton: {
            marginRight: 36,
        },
        hide: {
            display: 'none',
        },
        drawer: {
            width: drawerWidth,
            flexShrink: 0,
            whiteSpace: 'nowrap',
        },
        drawerOpen: {
            width: drawerWidth,
            transition: theme.transitions.create('width', {
                easing: theme.transitions.easing.sharp,
                duration: theme.transitions.duration.enteringScreen,
            }),
        },
        drawerClose: {
            transition: theme.transitions.create('width', {
                easing: theme.transitions.easing.sharp,
                duration: theme.transitions.duration.leavingScreen,
            }),
            overflowX: 'hidden',
            width: theme.spacing(7) + 1,
            [theme.breakpoints.up('sm')]: {
                width: theme.spacing(9) + 1,
            },
        },
        toolbar: {
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'flex-end',
            padding: theme.spacing(0, 1),
            // necessary for content to be below app bar
            ...theme.mixins.toolbar,
        },
        content: {
            flexGrow: 1,
            padding: theme.spacing(3),
        },
        active: {
            backgroundColor: theme.palette.action.selected
        },
        title: {
            flexGrow: 1,
        },
    }),
);


interface IMainLayoutProps {
    showSearchBar?: boolean
    showFooter?: boolean
}

export function MainLayout(props: PropsWithChildren<IMainLayoutProps>) {
    const classes = useStyles();
    const theme = useTheme();
    const dispatch = useDispatch();
    const currentUser = useSelector(currentUserSelector);
    const [open, setOpen] = React.useState(false);
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);

    const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleLogout = () => {
        SessionService.removeSessionGuid();
        dispatch(rootLayoutSlice.actions.logout());
        setAnchorEl(null);
        window.location.href= "/";
    };

    const handleClose = () => {
        setAnchorEl(null);

    };
    const handleDrawerOpen = () => {
        setOpen(true);
    };

    const handleDrawerClose = () => {
        setOpen(false);
    };

    const accountMenu = Boolean(anchorEl);


    return (
        <div className={classes.root}>
            <CssBaseline/>
            <AppBar
                position="fixed"
                className={clsx(classes.appBar, {
                    [classes.appBarShift]: open,
                })}
            >
                <Toolbar>
                    <IconButton
                        color="inherit"
                        aria-label="open drawer"
                        onClick={handleDrawerOpen}
                        edge="start"
                        className={clsx(classes.menuButton, {
                            [classes.hide]: open,
                        })}
                    >
                        <MenuIcon/>
                    </IconButton>
                    <Typography variant="h6" noWrap className={classes.title}>
                        Super Todo App
                    </Typography>

                    {
                        currentUser &&
                        <div>
                            <Button
                                aria-label="account of current user"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                                onClick={handleMenu}
                                color="inherit"
                            >
                                {currentUser.name}
                            </Button>
                            <Menu
                                id="menu-appbar"
                                anchorEl={anchorEl}
                                anchorOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                keepMounted
                                transformOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                open={accountMenu}
                                onClose={handleClose}
                            >
                                <MenuItem onClick={handleLogout}>Log out</MenuItem>
                            </Menu>
                        </div>

                    }
                </Toolbar>
            </AppBar>
            <Drawer
                variant="permanent"
                className={clsx(classes.drawer, {
                    [classes.drawerOpen]: open,
                    [classes.drawerClose]: !open,
                })}
                classes={{
                    paper: clsx({
                        [classes.drawerOpen]: open,
                        [classes.drawerClose]: !open,
                    }),
                }}
            >
                <div className={classes.toolbar}>
                    <IconButton onClick={handleDrawerClose}>
                        {theme.direction === 'rtl' ? <ChevronRightIcon/> : <ChevronLeftIcon/>}
                    </IconButton>
                </div>
                <Divider/>
                <List>

                    <ListItem button component={RouterLink} to="/users" activeClassName={classes.active}>
                        <ListItemIcon>
                            <PeopleIcon/>
                        </ListItemIcon>
                        <ListItemText primary="Users"/>
                    </ListItem>

                    <ListItem button component={RouterLink} to="/tasks" activeClassName={classes.active}>
                        <ListItemIcon>
                            <AssignmentIcon/>
                        </ListItemIcon>
                        <ListItemText primary="Tasks"/>
                    </ListItem>

                    <ListItem button component={RouterLink} to="/chat" activeClassName={classes.active}>
                        <ListItemIcon>
                            <Chat/>
                        </ListItemIcon>
                        <ListItemText primary="Messenger"/>
                    </ListItem>
                </List>
                <Divider/>

            </Drawer>
            <main className={classes.content}>
                <div className={classes.toolbar}/>
                {props.children}
                {
                    props.showFooter &&
                    <Box mt={8}>
                        <Copyright/>
                    </Box>
                }
            </main>
        </div>
    );
}