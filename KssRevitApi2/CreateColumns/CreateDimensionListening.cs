using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KssRevitApi2.CreateColumns
{
    public class CreateDimensionListening : IUpdater
    {
        private AddInId _appId;
        private UpdaterId _updaterId;
        public CreateDimensionListening(AddInId appId)
        {
            _appId = appId;
            _updaterId = new UpdaterId(appId, new Guid("f781a8da-bb26-48f2-8219-c086acd8b7cf"));
        }
        public void Execute(UpdaterData data)
        {
            Document doc = data.GetDocument();
            IEnumerable<ElementId> idsAdd= data.GetAddedElementIds();
            //IEnumerable<ElementId> idsModify= data.GetModifiedElementIds();
            if(idsAdd.Any())
            {
                foreach(ElementId id in idsAdd)
                {
                    Dimension dimension= doc.GetElement(id) as Dimension;
                    {
                        if(dimension != null)
                        {

                            /// todo 
                            /// 

                        }
                    }

                }
            }

        }

        public string GetAdditionalInformation()
        {
            return "NguyenVanPhong";
        }

        public ChangePriority GetChangePriority()
        {
            return ChangePriority.Annotations;
        }

        public UpdaterId GetUpdaterId()
        {
            return _updaterId;
        }

        public string GetUpdaterName()
        {
            return "ListenCreateDimension";
        }
    }
}
