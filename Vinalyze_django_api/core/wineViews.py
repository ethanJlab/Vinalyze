from django.http import HttpResponse, JsonResponse
from django.core import serializers
from .models import Wine
def getAllWineIds(request):
    ret = list(Wine.objects.all())
    ret_json = serializers.serialize('json',ret)
    return HttpResponse(ret_json,content_type='application/json')

def getWineById(request, wineID):
    ret = Wine.objects.get(id=wineID)
    ret_json = serializers.serialize('json', [ret])
    return HttpResponse(ret_json, content_type='application/json')

