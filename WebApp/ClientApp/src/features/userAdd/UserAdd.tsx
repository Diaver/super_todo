import React from 'react';
import {createStyles, makeStyles, Theme} from '@material-ui/core/styles';
import {Backdrop, Container, Fab, Grid, Typography} from "@material-ui/core";
import {useDispatch, useSelector} from "react-redux";
import SaveIcon from '@material-ui/icons/Save';
import CancelIcon from '@material-ui/icons/Cancel';
import {NavLink as RouterLink} from "react-router-dom";
import {AccountCircle} from "@material-ui/icons";
import {TextValidator, ValidatorForm} from "react-material-ui-form-validator";
import AlternateEmailIcon from '@material-ui/icons/AlternateEmail';
import TodayIcon from '@material-ui/icons/Today';
import {addAsync, loadingSelector, usersAddSlice, userSelector} from "./usersAddSlice";
import CircularProgress from '@material-ui/core/CircularProgress';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {},
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
        backdrop: {
            zIndex: theme.zIndex.drawer + 1,
            color: '#fff',
        },
    }),
);

export function UserAdd() {
    
    const classes = useStyles();
    const dispatch = useDispatch();
    const user = useSelector(userSelector);
    const loading = useSelector(loadingSelector);

    return (
        <Container component="main" maxWidth="md">
            
            <Backdrop className={classes.backdrop} open={loading}>
                <CircularProgress color="inherit" />
            </Backdrop>
            
            <ValidatorForm
                className={classes.form}
                onSubmit={() => {
                    dispatch(addAsync());
                }}
                onError={errors => console.log(errors)}
            >
                <Grid
                    container
                    spacing={3}
                    direction="row"
                    justify="space-between"
                    alignItems="center">
                    
                    <Grid item>
                        <Typography variant={"h4"}>
                            User Add
                        </Typography>
                    </Grid>
                    
                    <Grid item>
                        <Grid
                            spacing={1}
                            direction="row"
                            container>
                          
                            <Grid item>
                                <Fab aria-label="cancel" component={RouterLink} to="/users">
                                    <CancelIcon/>
                                </Fab>
                            </Grid>
                            <Grid item>
                                <Fab color="primary" aria-label="add" type="submit">
                                    <SaveIcon/>
                                </Fab>
                            </Grid>
                        </Grid>
                    </Grid>
                </Grid>


                <div className={classes.textBoxWithIconContainer}>
                    <AccountCircle/>
                    <TextValidator
                        variant="outlined"
                        name="Name"
                        className={classes.textBoxWithIcon}
                        fullWidth={true}
                        label="Name"
                        placeholder="Please enter user's name"
                        value={user.name}
                        onChange={(e: any) => dispatch(usersAddSlice.actions.updateFieldValue({
                            field: "name",
                            value: e.target.value
                        }))}
                        
                        validators={['required']}
                        errorMessages={["This field is required"]}

                    />

                </div>

                <div className={classes.textBoxWithIconContainer}>
                    <AlternateEmailIcon/>
                    <TextValidator
                        variant="outlined"
                        name="Email"
                        className={classes.textBoxWithIcon}
                        fullWidth
                        label="Email"
                        placeholder="Please enter user's email"
                        value={user.email}
                        onChange={(e: any) => dispatch(usersAddSlice.actions.updateFieldValue({
                            field: "email",
                            value: e.target.value
                        }))}
                        validators={['required', 'isEmail']}
                        errorMessages={["This field is required", 'Email is not valid']}
                    />
                </div>

                <div className={classes.textBoxWithIconContainer}>
                    <TodayIcon/>
                    <TextValidator
                        variant="outlined"
                        name="DateOfBirth"
                        className={classes.textBoxWithIcon}
                        fullWidth={true}
                        label="Date of Birth"
                        placeholder="Please enter user's date of birth"
                        value={user.dateOfBirth}
                        onChange={(e: any) => dispatch(usersAddSlice.actions.updateFieldValue({
                            field: "dateOfBirth",
                            value: e.target.value
                        }))}
                        validators={['required']}
                        errorMessages={["This field is required"]}
                    />
                    
                </div>
            </ValidatorForm>

        </Container>
    );
}