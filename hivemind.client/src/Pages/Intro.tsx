import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import Box from '@mui/material/Box';
import Divider from '@mui/material/Divider';
import Stack from '@mui/material/Stack';
import Paper from '@mui/material/Paper';

function Intro() {
  return (
      <Container>
          <Typography sx={{ m: 2 }} variant="h3" align="center">
            Welcome to HiveMind
          </Typography>
          <Divider color="#ffc400" />
          <Box sx={{ mt: 5 }} justifyItems="center">
              <Typography maxWidth="800px" >
                  HiveMind aims to be your one stop scheduler for emulating old broadcasts.
                  But there are tons of schedulers that aim to do the same thing, what sets HiveMind apart from other schedulers is "Orchestration"!
              </Typography>
              <Typography maxWidth="800px" sx={{mt: 1}}>
                  That's right HiveMind is meant to control other devices/apps on your network to offload the burden of actually playing the content.
              </Typography>
              <Typography maxWidth="800px" sx={{ mt: 1 }}>
                  HiveMind has a ton of awesome features that allows you to get as close to emulating original broadcasting as possible. 
              </Typography>
          </Box>
          <Box sx={{ mt: 4}} >
              <Stack direction="row" justifyContent="space-around">
                  <Paper sx={{ width: '30%'}}>
                      <Typography sx={{ m: 2 }} variant="h6" align="center">
                          Management
                      </Typography>
                      <Divider color="#ffc400" />
                      <Typography sx={{ m: 2 }}>
                          1. Create a library and specify the path to import your media.
                      </Typography>
                      <Typography sx={{ m: 2 }}>
                          2. Automation will pull in all of your data and add tags
                      </Typography>
                      <Typography sx={{ m: 2 }}>
                          3. Create Queries based on media item properties
                          and tags (Like smart collections)
                      </Typography>
                  </Paper>
                  <Paper sx={{ width: '30%' }}>
                      <Typography sx={{ m: 2 }} variant="h6" align="center">
                          Scheduling
                      </Typography>
                      <Divider color="#ffc400" />
                      <Typography sx={{ m: 2 }}>
                          1. Create a Schedule
                      </Typography>
                      <Typography sx={{ m: 2 }}>
                          2. Add items to the schedules, we currently support General Items and Blocks
                      </Typography>
                      <Typography sx={{ m: 2 }}>
                          3. Create a Channel add overlays or branding and assign a it Schedule.
                      </Typography>
                      <Typography sx={{ m: 2 }}>
                          4. Backend Processing will Create a SChedule from these settings.
                      </Typography>
                  </Paper>
                  <Paper sx={{ width: '30%' }}>
                      <Typography sx={{ m: 2 }} variant="h6" align="center">
                          Orchestrating
                      </Typography>
                      <Divider color="#ffc400" />
                      <Typography sx={{ m: 2 }}>
                          Coming Soon!
                      </Typography>
                  </Paper>
              </Stack>
          </Box>
          <Box sx={{ mt: 4 }} >
              <Typography sx={{ m: 2 }} variant="h6" align="center" >
                Features Coming Soon:
              </Typography>
              <Stack direction="row" justifyContent="space-around">
                  <Paper>
                      <Typography  sx={{ m: 2 }}>
                          Commercial Enhancements

                      </Typography>
                      <ul>
                          <li>Podding</li>
                          <li>Day Parting</li>
                      </ul>
                  </Paper>
                  <Paper>
                      <Typography sx={{ m: 2 }}>
                          Scheduling Enhancements
                      </Typography>
                      <ul>
                          <li>Events</li>
                          <li>Campaigns</li>
                      </ul>
                  </Paper>

              </Stack>
              
          </Box>
      </Container>
  );
}

export default Intro;