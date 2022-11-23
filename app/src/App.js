import EmployeeListing from './EmployeeListing'

const App = () => {
  return (
    <div className="container-fluid">
      <div className="row">
        <h2>Paylocity Benefits Calculator</h2>
      </div>
      <div className="row">
        <EmployeeListing></EmployeeListing>
      </div>
    </div>
  );
};

export default App;