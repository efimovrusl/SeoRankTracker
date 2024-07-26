import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from '@mui/material';
import { WebsiteRankDto } from '../services/Api';

interface ResultsTableProps {
  results: WebsiteRankDto[];
}

export default function ResultsTable({ results }: ResultsTableProps) {
  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Search Keyword</TableCell>
            <TableCell>Website URL</TableCell>
            <TableCell>Result URL</TableCell>
            <TableCell>Description</TableCell>
            <TableCell>Position</TableCell>
            <TableCell>Date</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {results.map((result) => (
            <TableRow key={result.resultUrl}>
              <TableCell>{result.searchKeyword}</TableCell>
              <TableCell>{result.websiteUrl}</TableCell>
              <TableCell>{result.resultUrl}</TableCell>
              <TableCell>{result.description}</TableCell>
              <TableCell>{result.position}</TableCell>
              <TableCell>{new Date(result.date).toLocaleDateString()}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};
