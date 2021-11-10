import * as React from 'react';
import {Button, Grid, TextField} from "@mui/material";
import {Send} from "@material-ui/icons";

export function SendMessage() {

    return (
        <div>
            <Grid container spacing={2}>
                <Grid item xs={10}>
                    <TextField
                        id="outlined-multiline-static"
                        placeholder="Enter message"
                        multiline
                        rows={4}
                        fullWidth
                    />
                </Grid>
                <Grid item xs={2} alignSelf="center">
                    <Button variant="contained" endIcon={<Send/>} size="large"
                            onClick={() => {
                                console.log("message sent")
                            }}>
                        Send
                    </Button>
                </Grid>
            </Grid>
        </div>
    );
}