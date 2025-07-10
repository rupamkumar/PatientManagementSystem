DECLARE @xdoc as xml
SET @xdoc  = '<Root>
  <Visit>
    <visitId>2968de9e-233c-4b87-bb92-ee946366fac7</visitId>
  </Visit>
  <Visit>
    <visitId>d9def061-3ea5-443d-82d9-e0eebd44d378</visitId>
  </Visit>
</Root>'

select @xdoc

select x.Rec.query('visitId').value('.', 'nvarchar(max)') as 'VisitId'  from @xdoc.nodes('/Root/Visit') as x(Rec) 