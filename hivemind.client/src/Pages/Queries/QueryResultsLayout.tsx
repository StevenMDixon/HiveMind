//import { IconButton, Stack } from "@mui/material";
//import AddIcon from '@mui/icons-material/Add';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import type { Media, Show } from '../../Types/Media';
import Paper from '@mui/material/Paper';
import { type CellData, CustomTable } from '../Components/Table';

export const QueryResultsLayout = ({ testResultsPromise }: { testResultsPromise: Promise<Media[]>}) => {
    //const results = use(testResultsPromise);
    const columns = [
        { key: 'title', name: 'Title', align: 'left' },
        { key: 'mediaType', name: 'Type', align: 'center' },
        { key: 'duration', name: 'Duration', align: 'right', format: (duration: number) => (duration / 1000 / 60).toFixed(5) + ' (M)' },
        { key: 'width', name: 'Width', align: 'right', format: (width: string) => width + " px" },
        { key: 'height', name: 'Height', align: 'right', format: (height: string) => height + " px" },
        { key: 'show', name: 'Show', align: 'center', format: (show: Show) => (show?.name) },
        { key: 'episodeNumber', name: 'Episode', align: 'center' },
        { key: 'seasonNumber', name: 'Season', align: 'center' },
    ] as CellData<Media>[];

    const handleRetry = () => { }

  return (
      <Container sx={{ mt: 3, p: 2} }>
          <Paper sx={{ p: 2 }} >
              <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                  Query Results Sample:
              </Typography>

              <CustomTable dataPromise={testResultsPromise} columns={columns} handleRetry={handleRetry} />
        </Paper>
      </Container>
  );
}

export default QueryResultsLayout;